using App.Modules.Sys.Shared.Models;
using System;

namespace App.Modules.Sys.Domain.Domains.Configuration
{
    /// <summary>
    /// Represents a stored configuration setting value with 5-tier hierarchy.
    /// Implements IHasKey and IHasSerializedTypeValueNullable for consistency.
    /// 
    /// Hierarchy (most specific to least specific):
    /// 1. User (individual preferences)
    /// 2. Workspace (tenant/organization)
    /// 3. Distributor (regional partner/licensee)
    /// 4. Provider (platform operator/hosting company)
    /// 5. Developer (software defaults)
    /// 
    /// Use '*' wildcard for any tier to indicate "applies to all at this level".
    /// Example: DeveloperId='*', ProviderId='*', DistributorId='eu', WorkspaceId='*', UserId='*'
    ///   = Setting applies to ALL workspaces/users under EU distributor
    /// 
    /// Database schema:
    /// - DeveloperId: Guid or '*' (software author defaults)
    /// - ProviderId: Guid or '*' (platform operator)  
    /// - DistributorId: Guid or '*' (regional partner)
    /// - WorkspaceId: Guid or '*' (tenant/organization)
    /// - UserId: Guid or '*' (individual user)
    /// - Key: Hierarchical path like 'Appearance/Theme/PrimaryColor'
    /// - SerializedTypeName: .NET type name
    /// - SerializedTypeValue: JSON or string representation
    /// - IsLocked: Prevents descendant levels from overriding (for compliance/data sovereignty)
    /// 
    /// See ADR-001-5-TIER-HIERARCHICAL-SETTINGS-SYSTEM.md for rationale.
    /// </summary>
    public class SettingValue : IHasKey, IHasSerializedTypeValueNullable
    {
        /// <summary>
        /// Developer ID (software author) or '*' for default.
        /// Represents base software defaults set by the development team.
        /// </summary>
        public string DeveloperId { get; set; } = "*";

        /// <summary>
        /// Provider ID (platform operator/hosting company) or '*' for all providers.
        /// Represents platform-level configuration (e.g., BASE Platform operated by your company).
        /// </summary>
        public string ProviderId { get; set; } = "*";

        /// <summary>
        /// Distributor ID (regional partner/licensee) or '*' for all distributors.
        /// Represents regional-level configuration (e.g., BASE France, BASE Germany).
        /// Critical for data sovereignty and regional compliance requirements.
        /// </summary>
        public string DistributorId { get; set; } = "*";

        /// <summary>
        /// Workspace ID (tenant/organization) or '*' for all workspaces.
        /// Represents tenant-specific configuration (e.g., Acme Corporation).
        /// </summary>
        public string WorkspaceId { get; set; } = "*";

        /// <summary>
        /// User ID (individual) or '*' for all users.
        /// Represents personal preferences (e.g., John Doe's theme preference).
        /// </summary>
        public string UserId { get; set; } = "*";

        /// <inheritdoc/>
        public string Key { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string SerializedTypeName { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string? SerializedTypeValue { get; set; } = string.Empty;

        /// <summary>
        /// When true, descendant levels cannot override this setting.
        /// Example: Workspace admin locks 'Appearance/' ? users can't change appearance settings
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// When this setting was last modified
        /// </summary>
        public DateTime LastModified { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Who last modified this setting
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Determine the setting level based on the hierarchy.
        /// Returns the MOST SPECIFIC level this setting applies to.
        /// </summary>
        public SettingLevel Level
        {
            get
            {
                // Most specific first:
                if (UserId != "*")
                {
                    return SettingLevel.User;
                }
                if (WorkspaceId != "*")
                {
                    return SettingLevel.Workspace;
                }
                if (DistributorId != "*")
                {
                    return SettingLevel.Distributor;
                }
                if (ProviderId != "*")
                {
                    return SettingLevel.Provider;
                }
                return SettingLevel.Developer;
            }
        }
    }
}
