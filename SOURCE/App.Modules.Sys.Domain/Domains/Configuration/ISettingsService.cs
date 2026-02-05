using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Domain.Domains.Configuration
{
    /// <summary>
    /// Service for managing hierarchical configuration settings.
    /// Implements the hierarchy: System ? Workspace ? Person
    /// with wildcard support ('*') and locking mechanism.
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Get effective setting value, walking up the hierarchy if not found at current level.
        /// Search order: Person ? Workspace ? System ? Default
        /// </summary>
        /// <param name="key">Setting key (e.g., 'Appearance/Background/Color')</param>
        /// <param name="workspaceId">Workspace ID or null for system-level</param>
        /// <param name="userId">User ID or null for workspace-level</param>
        /// <param name="defaultValue">Default value if not found</param>
        /// <param name="ct">Cancellation token</param>
        Task<T?> GetValueAsync<T>(
            string key, 
            Guid? workspaceId = null, 
            Guid? userId = null,
            T? defaultValue = default,
            CancellationToken ct = default);

        /// <summary>
        /// Set setting value at appropriate level.
        /// Checks if locked before allowing change.
        /// </summary>
        /// <param name="key">Setting key</param>
        /// <param name="value">Value to set</param>
        /// <param name="workspaceId">Workspace ID (null = system-level)</param>
        /// <param name="userId">User ID (null = workspace-level)</param>
        /// <param name="modifiedBy">Who made the change</param>
        /// <param name="ct">Cancellation token</param>
        Task SetValueAsync<T>(
            string key, 
            T value, 
            Guid? workspaceId = null, 
            Guid? userId = null,
            string? modifiedBy = null,
            CancellationToken ct = default);

        /// <summary>
        /// Reset setting to default (delete override).
        /// If resetChildren=true, also resets all child settings (e.g., 'Appearance/*')
        /// </summary>
        /// <param name="key">Setting key or pattern</param>
        /// <param name="workspaceId">Workspace ID</param>
        /// <param name="userId">User ID</param>
        /// <param name="resetChildren">Whether to reset child settings</param>
        /// <param name="ct">Cancellation token</param>
        Task ResetAsync(
            string key, 
            Guid? workspaceId = null, 
            Guid? userId = null,
            bool resetChildren = false,
            CancellationToken ct = default);

        /// <summary>
        /// Lock a setting, preventing descendant levels from changing it.
        /// Locking 'Appearance/' locks all child settings like 'Appearance/Background'
        /// </summary>
        /// <param name="key">Setting key or pattern</param>
        /// <param name="workspaceId">Workspace ID (only System and Workspace can lock)</param>
        /// <param name="locked">True to lock, false to unlock</param>
        /// <param name="modifiedBy">Who made the change</param>
        /// <param name="ct">Cancellation token</param>
        Task SetLockAsync(
            string key, 
            Guid? workspaceId, 
            bool locked,
            string? modifiedBy = null,
            CancellationToken ct = default);

        /// <summary>
        /// Check if a setting is locked (prevents changes at descendant levels).
        /// Checks hierarchically: if parent is locked, child is locked.
        /// </summary>
        /// <param name="key">Setting key</param>
        /// <param name="workspaceId">Workspace ID</param>
        /// <param name="userId">User ID</param>
        /// <param name="ct">Cancellation token</param>
        Task<bool> IsLockedAsync(
            string key, 
            Guid? workspaceId = null, 
            Guid? userId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get all settings at a specific level
        /// </summary>
        Task<IEnumerable<SettingValue>> GetAllAsync(
            SettingLevel level, 
            Guid? workspaceId = null, 
            Guid? userId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get all child settings under a parent key (e.g., all under 'Appearance/')
        /// </summary>
        Task<IEnumerable<SettingValue>> GetChildrenAsync(
            string parentKey, 
            Guid? workspaceId = null, 
            Guid? userId = null,
            CancellationToken ct = default);
    }
}
