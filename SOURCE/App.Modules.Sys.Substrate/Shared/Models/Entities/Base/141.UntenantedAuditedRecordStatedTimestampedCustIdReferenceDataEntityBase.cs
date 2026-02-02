using App.Modules.Sys.Shared.Models.Persistence;

namespace App.Modules.Sys.Shared.Models.Entities.Base
{
    /// <summary>
    /// 
    /// <para>
    /// Note that this Base runs *parrallel* to
    /// <see cref="UntenantedAuditedRecordStatedTimestampedGuidIdReferenceDataEntityBase"/>
    /// (as one can't define a Generic Id and then redefine it with a Guid Id).
    /// </para>
    /// 
    /// <para>
    /// Does Not Implements:
    /// <list type="bullet">
    /// <item><see cref="IHasGuidId"/></item>,
    /// <item><see cref="IHasTenantFK"/></item>,
    /// </list>
    /// </para>
    /// 
    /// <para>
    /// Does Implement:
    /// <list type="bullet">
    /// <item><see cref="IHasTimestampRecordStateInRecordAuditability"/></item>,
    /// <item><see cref="IHasReferenceDataOfGenericIdEnabledTitleDescImage{TId}"/></item>,
    /// </list>
    /// </para>    
    /// 
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public abstract class UntenantedAuditedRecordStatedTimestampedCustomIdReferenceDataEntityBase<TId> :
        UntenantedAuditedRecordStatedTimestampedCustIdEntityBase<TId>,
       /*Enherited: IHasId<Guid>, IHasTimestamp, IHasInRecordAuditability, IHasRecordState*/
       IHasReferenceDataOfGenericIdEnabledTitleDescImage<TId>
        where TId : struct
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected UntenantedAuditedRecordStatedTimestampedCustomIdReferenceDataEntityBase()
        {
            //As the Id is custom, cannot set:
            // NO: Id = GuidFactory.NewGuid();
        }




        /// <inheritdoc/>
        public virtual bool Enabled { get; set; }
        /// <inheritdoc/>

        public virtual string Title { get; set; } = string.Empty;
        /// <inheritdoc/>

        public virtual string Description { get; set; } = string.Empty;


        /// <inheritdoc/>

        public virtual string? ImageUrl { get; set; } = string.Empty;

        /// <inheritdoc/>
        public virtual string? ImageStyle { get; set; }
    }


}
