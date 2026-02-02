using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Modules.Sys.Infrastructure.Data.Entities
{
    /// <summary>
    /// Database entity for hierarchical configuration settings.
    /// Supports System/Workspace/Person levels with wildcards and locking.
    /// </summary>
    [Table("ConfigurationSettings")]
    public class ConfigurationSettingEntity
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Workspace ID or '*' for system-level (all workspaces)
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Column("WorkspaceId")]
        public string WorkspaceId { get; set; } = "*";

        /// <summary>
        /// User ID or '*' for workspace-level (all users in workspace)
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Column("UserId")]
        public string UserId { get; set; } = "*";

        /// <summary>
        /// Hierarchical setting key (e.g., 'Appearance/Background/Color')
        /// </summary>
        [Required]
        [MaxLength(500)]
        [Column("Key")]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// .NET type name for deserialization (e.g., 'System.String')
        /// </summary>
        [Required]
        [MaxLength(500)]
        [Column("Type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Serialized value (JSON or string representation)
        /// </summary>
        [Required]
        [Column("SerializedValue", TypeName = "nvarchar(max)")]
        public string SerializedValue { get; set; } = string.Empty;

        /// <summary>
        /// Lock flag - prevents descendant levels from overriding.
        /// Locking 'Appearance/' locks all children like 'Appearance/Background'
        /// </summary>
        [Column("IsLocked")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// When this setting was last modified
        /// </summary>
        [Column("LastModified")]
        public DateTime LastModified { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Who last modified this setting (user ID or 'SYSTEM')
        /// </summary>
        [MaxLength(100)]
        [Column("ModifiedBy")]
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Module that owns this setting (for filtering/organization)
        /// </summary>
        [MaxLength(100)]
        [Column("Module")]
        public string? Module { get; set; }

        // Composite index for efficient lookups
        // Index: (WorkspaceId, UserId, Key) - covers most query patterns
    }
}
