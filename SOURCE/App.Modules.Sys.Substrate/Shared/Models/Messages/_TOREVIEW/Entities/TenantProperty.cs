// using System;
using App.Modules.Sys.Shared.Models.Entities.Base;
using App.Modules.Sys.Shared.Models.Persistence;

namespace App.Modules.Sys.Shared.Models.Messages._TOREVIEW.Entities
{
    /// <summary>
    ///  System entity (not exposed to the system's exterior) for
    /// a property of a <see cref="Tenant"/> in the system.
    /// </summary>
    public class TenantProperty
        : UntenantedAuditedRecordStatedTimestampedGuidIdEntityBase,
        IHasOwnerFK,
        //NO: IHasTenantFK,
        IHasKeyGenericValue<string>
    {
        /// <summary>
        /// The Key of the property.
        /// </summary>
        public virtual string Key { get; set; } = string.Empty;
        /// <summary>
        /// The string value of the property.
        /// </summary>
        public virtual string Value { get; set; } = string.Empty;
        /// <summary>
        /// The FK of the parent <see cref="Tenant"/>.
        /// </summary>
        public virtual Guid TenantFK { get; set; }

        /// <summary>
        /// The FK of the parent <see cref="Tenant"/>
        /// </summary>
        /// <returns></returns>
        public Guid GetOwnerFk()
        {
            return TenantFK;
        }

    }
}