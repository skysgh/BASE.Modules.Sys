using App.Modules.Sys.Domain.Configuration;
using App.Modules.Sys.Infrastructure.Services.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Infrastructure.Services.Configuration
{
    /// <summary>
    /// Implementation of hierarchical settings service.
    /// Manages System/Workspace/Person levels with locking and caching.
    /// </summary>
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _repository;
        private readonly ICacheRegistry _cache;

        public SettingsService(ISettingsRepository repository, ICacheRegistry cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<T?> GetValueAsync<T>(
            string key, 
            Guid? workspaceId = null, 
            Guid? userId = null, 
            T? defaultValue = default, 
            CancellationToken ct = default)
        {
            var cacheKey = BuildCacheKey(key, workspaceId, userId);

            // Try cache first
            var cached = await _cache.GetOrCreateAsync(
                cacheKey,
                async (ct) =>
                {
                    // Walk up hierarchy: Person ? Workspace ? System ? Default
                    SettingValue? setting = null;

                    // Try Person level
                    if (userId.HasValue && workspaceId.HasValue)
                    {
                        setting = await _repository.GetAsync(key, workspaceId.Value.ToString(), userId.Value.ToString(), ct);
                    }

                    // Try Workspace level
                    if (setting == null && workspaceId.HasValue)
                    {
                        setting = await _repository.GetAsync(key, workspaceId.Value.ToString(), "*", ct);
                    }

                    // Try System level
                    if (setting == null)
                    {
                        setting = await _repository.GetAsync(key, "*", "*", ct);
                    }

                    // Deserialize or return default
                    if (setting != null)
                    {
                        return JsonSerializer.Deserialize<T>(setting.SerializedValue);
                    }

                    return defaultValue;
                },
                expiry: TimeSpan.FromMinutes(15),
                ct: ct
            );

            return cached;
        }

        public async Task SetValueAsync<T>(
            string key, 
            T value, 
            Guid? workspaceId = null, 
            Guid? userId = null, 
            string? modifiedBy = null, 
            CancellationToken ct = default)
        {
            // Check if locked
            if (await IsLockedAsync(key, workspaceId, userId, ct))
            {
                throw new InvalidOperationException($"Setting '{key}' is locked and cannot be modified");
            }

            var setting = new SettingValue
            {
                WorkspaceId = workspaceId?.ToString() ?? "*",
                UserId = userId?.ToString() ?? "*",
                Key = key,
                Type = typeof(T).FullName ?? typeof(T).Name,
                SerializedValue = JsonSerializer.Serialize(value),
                LastModified = DateTime.UtcNow,
                ModifiedBy = modifiedBy
            };

            await _repository.SetAsync(setting, ct);

            // Invalidate cache
            _cache.RemoveByPattern(BuildCacheKeyPattern(key));
        }

        public async Task ResetAsync(
            string key, 
            Guid? workspaceId = null, 
            Guid? userId = null, 
            bool resetChildren = false, 
            CancellationToken ct = default)
        {
            var workspaceIdStr = workspaceId?.ToString() ?? "*";
            var userIdStr = userId?.ToString() ?? "*";

            if (resetChildren)
            {
                // Delete all settings under this key (e.g., "Appearance/*")
                await _repository.DeleteByPatternAsync(key, workspaceIdStr, userIdStr, ct);
            }
            else
            {
                // Delete just this specific key
                await _repository.DeleteAsync(key, workspaceIdStr, userIdStr, ct);
            }

            // Invalidate cache
            if (resetChildren)
            {
                _cache.RemoveByPattern($"{key}*");
            }
            else
            {
                _cache.RemoveByPattern(BuildCacheKeyPattern(key));
            }
        }

        public async Task SetLockAsync(
            string key, 
            Guid? workspaceId, 
            bool locked, 
            string? modifiedBy = null, 
            CancellationToken ct = default)
        {
            // Only System and Workspace levels can lock
            if (workspaceId == null)
            {
                // System level lock
                await _repository.SetLockAsync(key, "*", "*", locked, modifiedBy, ct);
            }
            else
            {
                // Workspace level lock
                await _repository.SetLockAsync(key, workspaceId.Value.ToString(), "*", locked, modifiedBy, ct);
            }

            // Invalidate cache
            _cache.RemoveByPattern(BuildCacheKeyPattern(key));
        }

        public async Task<bool> IsLockedAsync(
            string key, 
            Guid? workspaceId = null, 
            Guid? userId = null, 
            CancellationToken ct = default)
        {
            // Check hierarchically: parent locks apply to children
            var keyParts = key.Split('/');
            var currentPath = "";

            foreach (var part in keyParts)
            {
                currentPath = string.IsNullOrEmpty(currentPath) ? part : $"{currentPath}/{part}";

                // Check System level
                var systemLock = await _repository.IsLockedAsync(currentPath, "*", "*", ct);
                if (systemLock) return true;

                // Check Workspace level
                if (workspaceId.HasValue)
                {
                    var workspaceLock = await _repository.IsLockedAsync(currentPath, workspaceId.Value.ToString(), "*", ct);
                    if (workspaceLock) return true;
                }
            }

            return false;
        }

        public async Task<IEnumerable<SettingValue>> GetAllAsync(
            SettingLevel level, 
            Guid? workspaceId = null, 
            Guid? userId = null, 
            CancellationToken ct = default)
        {
            var workspaceIdStr = level == SettingLevel.System ? "*" : workspaceId?.ToString() ?? "*";
            var userIdStr = level == SettingLevel.Person ? userId?.ToString() ?? "*" : "*";

            return await _repository.GetAllAsync(workspaceIdStr, userIdStr, ct);
        }

        public async Task<IEnumerable<SettingValue>> GetChildrenAsync(
            string parentKey, 
            Guid? workspaceId = null, 
            Guid? userId = null, 
            CancellationToken ct = default)
        {
            var workspaceIdStr = workspaceId?.ToString() ?? "*";
            var userIdStr = userId?.ToString() ?? "*";

            return await _repository.GetChildrenAsync(parentKey, workspaceIdStr, userIdStr, ct);
        }

        private static string BuildCacheKey(string key, Guid? workspaceId, Guid? userId)
        {
            return $"setting:{workspaceId?.ToString() ?? "*"}:{userId?.ToString() ?? "*"}:{key}";
        }

        private static string BuildCacheKeyPattern(string key)
        {
            return $"setting:*:*:{key}";
        }
    }

    /// <summary>
    /// Repository interface for settings persistence.
    /// Implement this with your data access technology (EF Core, Dapper, etc.)
    /// </summary>
    public interface ISettingsRepository
    {
        Task<SettingValue?> GetAsync(string key, string workspaceId, string userId, CancellationToken ct);
        Task SetAsync(SettingValue setting, CancellationToken ct);
        Task DeleteAsync(string key, string workspaceId, string userId, CancellationToken ct);
        Task DeleteByPatternAsync(string keyPattern, string workspaceId, string userId, CancellationToken ct);
        Task SetLockAsync(string key, string workspaceId, string userId, bool locked, string? modifiedBy, CancellationToken ct);
        Task<bool> IsLockedAsync(string key, string workspaceId, string userId, CancellationToken ct);
        Task<IEnumerable<SettingValue>> GetAllAsync(string workspaceId, string userId, CancellationToken ct);
        Task<IEnumerable<SettingValue>> GetChildrenAsync(string parentKey, string workspaceId, string userId, CancellationToken ct);
    }
}
