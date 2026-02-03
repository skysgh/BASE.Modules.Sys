using App.Modules.Sys.Shared.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace App.Modules.Sys.Domain.Authorization
{
    /// <summary>
    /// System-level permission.
    /// These are runtime authorization checks for system administration.
    /// NOT the same as application-level roles/permissions (those live in Social/Work domains).
    /// Examples: "System.Configure", "Database.Migrate", "Settings.Edit"
    /// </summary>
    [SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix", 
        Justification = "SystemPermission is the correct domain term - this IS a permission entity")]
    public class SystemPermission : IHasKey, IHasTitleAndDescription
    {
        /// <summary>
        /// Unique permission key (e.g., "System.Configure")
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
        /// Category for grouping in UI (e.g., "System", "Database", "Settings")
        /// </summary>
        public string Category { get; set; } = "System";

        /// <summary>
        /// When this permission was defined
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Users who have this permission
        /// </summary>
        public List<UserSystemPermission> UserPermissions { get; set; } = new();
    }
}
