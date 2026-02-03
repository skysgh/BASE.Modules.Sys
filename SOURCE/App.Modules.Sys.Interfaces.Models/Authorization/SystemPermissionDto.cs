using System;

namespace App.Modules.Sys.Interfaces.Models.Authorization
{
    /// <summary>
    /// DTO for SystemPermission (API contract).
    /// </summary>
    public class SystemPermissionDto
    {
        /// <summary>
        /// Permission key (e.g., "System.Configure")
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Display title
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// What this permission grants
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Category for grouping
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// When permission was defined
        /// </summary>
        public string CreatedAt { get; set; } = string.Empty;
    }
}
