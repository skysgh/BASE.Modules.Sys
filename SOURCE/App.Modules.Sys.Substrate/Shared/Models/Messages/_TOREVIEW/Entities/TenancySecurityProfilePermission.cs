using App.Modules.Sys.Shared.Models.Entities.Base;

namespace App.Modules.Sys.Shared.Models.Messages._TOREVIEW.Entities
{
    /// <summary>
    /// A Permission to assign to a Tenancy Security Profile
    /// <para>
    /// TODO: Improve documentation to explain purpose
    /// </para>
    /// </summary>
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
    public class TenancySecurityProfilePermission : TenantFKAuditedRecordStatedTimestampedGuidIdEntityBase, IHasTitleAndDescription
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
    {
        /// <summary>
        /// The Title of the Permission
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Title { get; set; } = default!;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// The Description of the Permission
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }

}

