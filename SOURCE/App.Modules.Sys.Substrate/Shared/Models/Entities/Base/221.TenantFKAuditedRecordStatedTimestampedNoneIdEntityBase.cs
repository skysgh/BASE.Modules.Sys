using App.Modules.Sys.Shared.Models.Persistence;

namespace App.Modules.Sys.Shared.Models.Entities.Base
{
    /// <summary>
    /// Abstract base class for system entities
    /// 
    /// <para>
    /// Does not implement
    /// <list type="bullet">
    /// <item><see cref="IHasId{T}"/></item>
    /// <item><see cref="IHasGuidId"/></item>
    /// </list>
    /// </para>
    /// 
    /// <para>
    /// Does implement:
    /// <list type="bullet">
    /// <item><see cref="IHasTimestampRecordStateInRecordAuditability"/></item>
    /// </list>
    /// </para>
    /// </summary>
    public abstract class TenantFKAuditedRecordStatedTimestampedNoIdEntityBase :
        //Inheriting from the *untenanted* version
        UntenantedAuditedRecordStatedTimestampedNoneIdEntityBase,
        IHasTenantFK
    {
        /// <summary>
        /// Gets or sets the FK of the Tenancy this mutable model belongs to.
        /// </summary>
        /// Took off virtual ... See if it still works.
        public Guid TenantFK { get; set; }
    }


}