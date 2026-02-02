using App.Modules.Sys.Shared.Models.Entities.Base;

namespace App.Modules.Sys.Shared.Models.Messages._TOREVIEW.Entities.TenancySpecific
{
    /// <summary>
    /// A Permission that can be assigned directly to a Security Profile.
    /// </summary>
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
    public class PrincipalSecurityProfilePermission : TenantFKAuditedRecordStatedTimestampedGuidIdEntityBase, IHasTitleAndDescription
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
    {
        /// <summary>
        /// It's title
        /// </summary>
        public string Title { get; set; } = default!;

        /// <summary>
        /// It's Description
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }

}

