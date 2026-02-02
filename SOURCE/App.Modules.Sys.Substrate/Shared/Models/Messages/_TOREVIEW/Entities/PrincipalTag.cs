using App.Modules.Sys.Shared.Models.Entities.Base;

namespace App.Modules.Sys.Shared.Models.Messages._TOREVIEW.Entities
{
    /// <summary>
    /// System entity (not exposed to the system's exterior) for
    /// a Tag associated to a <see cref="Principal"/>
    /// </summary>
    public class PrincipalTag : UntenantedAuditedRecordStatedTimestampedGuidIdReferenceDataEntityBase
    {
    }
}