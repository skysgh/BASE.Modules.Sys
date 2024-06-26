﻿
namespace App.Modules.TmpSys.Shared.Models.TODO.Entities
{
    using App.Modules.TmpSys.Substrate.Models.Contracts;
    using App.Modules.TmpSys.Substrate.tmp.Models.Entities.Base;
    using System;

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
        public virtual string Key { get; set; }
        /// <summary>
        /// The string value of the property.
        /// </summary>
        public virtual string Value { get; set; }
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