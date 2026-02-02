using System;

namespace App.Modules.Sys.Domain.Configuration
{
    /// <summary>
    /// Represents a stored configuration setting value.
    /// Database schema:
    /// - WorkspaceId: Guid or '*' for all workspaces
    /// - UserId: Guid or '*' for all users  
    /// - Key: Hierarchical path like 'Appearance/Background/Color'
    /// - Type: .NET type name
    /// - SerializedValue: JSON or string representation
    /// - IsLocked: Prevents descendant levels from overriding
    /// </summary>
    public class SettingValue
    {
        /// <summary>
        /// Workspace ID or '*' for system-level
        /// </summary>
        public string WorkspaceId { get; set; } = "*";

        /// <summary>
        /// User ID or '*' for workspace-level
        /// </summary>
        public string UserId { get; set; } = "*";

        /// <summary>
        /// Hierarchical setting key (e.g., 'Appearance/Background/Color')
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// .NET type name (e.g., 'System.String', 'System.Int32')
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Serialized value (JSON or string)
        /// </summary>
        public string SerializedValue { get; set; } = string.Empty;

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
        /// Determine the setting level based on WorkspaceId and UserId
        /// </summary>
        public SettingLevel Level
        {
            get
            {
                if (WorkspaceId == "*" && UserId == "*") return SettingLevel.System;
                if (UserId == "*") return SettingLevel.Workspace;
                return SettingLevel.Person;
            }
        }
    }
}
