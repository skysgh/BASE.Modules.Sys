// using System;
using App.Modules.Sys.Shared.Models.Entities.Base;
using App.Modules.Sys.Shared.Models.Persistence;

namespace App.Modules.Sys.Shared.Models.Messages._TOREVIEW.Entities
{
    /// <summary>
    ///  System entity (not exposed to the system's exterior) for
    /// A Claim about a Tenant in the system.
    /// </summary>
    public class TenantClaim
        : UntenantedAuditedRecordStatedTimestampedGuidIdEntityBase,
        IHasOwnerFK
    {
        /// <summary>
        /// The Authority backing the Claim
        /// </summary>
        public virtual string? Authority { get; set; }
        /// <summary>
        /// The signature of the Authority
        /// </summary>
        public virtual string? AuthoritySignature { get; set; }
        /// <summary>
        /// The Claim's key
        /// </summary>
        public virtual string Key { get; set; } = string.Empty;
        /// <summary>
        /// The Claim's string value
        /// </summary>
        public virtual string Value { get; set; } = string.Empty;
        /// <summary>
        /// The FK of the parent <see cref="Tenant"/>
        /// </summary>
        public virtual Guid TenantFK { get; set; }

        /// <summary>
        /// Get the FK of the parent Tenant
        /// </summary>
        /// <returns></returns>
        public Guid GetOwnerFk()
        {
            return TenantFK;
        }
    }
}