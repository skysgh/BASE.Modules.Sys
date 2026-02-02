using App.Modules.Sys.Shared.Models.Persistence;

namespace App.Modules.Sys.Shared.Models.Entities.Base
{
    /// <summary>
    /// 
    /// <para>
    /// Does Not Implements:
    /// <list type="bullet">
    /// <item><see cref="IHasTenantFK"/></item>,
    /// </list>
    /// </para>
    /// 
    /// <para>
    /// Does Implement:
    /// <list type="bullet">
    /// <item><see cref="IHasGuidId"/></item>,
    /// <item><see cref="IHasTimestampRecordStateInRecordAuditability"/></item>,
    /// <item><see cref="IHasReferenceDataOfGenericIdEnabledTitleDescImage{TId}"/></item>,
    /// <item><see cref="IHasKeyGenericValue{T}"/></item>
    /// </list>
    /// </para>    
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public abstract class UntenantedAuditedRecordStatedTimestampedGuidIdReferenceDataKeyGenValueEntityBase<TValue> :
    UntenantedAuditedRecordStatedTimestampedGuidIdReferenceDataEntityBase,
    IHasReferenceDataOfGuidIdEnabledTitleDescImageKeyGenValue<TValue>
    where TValue : struct
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected UntenantedAuditedRecordStatedTimestampedGuidIdReferenceDataKeyGenValueEntityBase() : base()
        {
        }
        /// <inheritdoc/>
        public string Key { get; set; }=string.Empty;

        /// <inheritdoc/>
        public TValue Value { get; set; }
    }
}
