using App.Modules.Sys.Shared.Models.Entities.Base;
using App.Modules.Sys.Shared.Models.Persistence;

namespace App.Modules.Sys.Shared.Models.Messages._TOREVIEW.Entities
{
    /// <summary>
    /// System entity (not exposed to the system's exterior) for
    /// a property of the parent <see cref="Principal"/>
    /// </summary>
    public class PrincipalProperty
        : UntenantedAuditedRecordStatedTimestampedGuidIdEntityBase,
        IHasOwnerFK
    {
        /// <summary>
        /// THe unique key of the property.
        /// </summary>
        public virtual string Key { get; set; } = string.Empty;
        /// <summary>
        /// The value of the property.
        /// </summary>
        public virtual string Value { get; set; } = string.Empty;
        /// <summary>
        /// The FK of the parent <see cref="Principal"/>
        /// </summary>
        public virtual Guid PrincipalFK { get; set; }


        /// <summary>
        /// Return the Parent <see cref="Principal"/>'s FK.
        /// </summary>
        /// <returns></returns>
        public Guid GetOwnerFk()
        {
            return PrincipalFK;
        }
    }
}