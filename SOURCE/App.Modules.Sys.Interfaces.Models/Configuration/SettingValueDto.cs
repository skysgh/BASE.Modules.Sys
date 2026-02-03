namespace App.Modules.Sys.Interfaces.Models.Configuration
{
    /// <summary>
    /// DTO for SettingValue (API contract).
    /// </summary>
    public class SettingValueDto
    {
        /// <summary>
        /// Setting key
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Scope (System, Workspace, User)
        /// </summary>
        public string Scope { get; set; } = string.Empty;

        /// <summary>
        /// Scope ID (WorkspaceId or UserId)
        /// </summary>
        public string? ScopeId { get; set; }

        /// <summary>
        /// Setting value
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Value type (string, int, bool, json)
        /// </summary>
        public string ValueType { get; set; } = string.Empty;

        /// <summary>
        /// Whether value is locked (can't be overridden)
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// When setting was last updated
        /// </summary>
        public string UpdatedAt { get; set; } = string.Empty;
    }
}
