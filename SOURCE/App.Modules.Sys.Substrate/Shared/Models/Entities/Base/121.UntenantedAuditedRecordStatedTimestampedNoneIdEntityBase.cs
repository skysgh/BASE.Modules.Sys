using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using App.Modules.Sys.Shared.Models.Enums;
using App.Modules.Sys.Shared.Models.Persistence;

namespace App.Modules.Sys.Shared.Models.Entities.Base
{
    /// <summary>
    /// Abstract base class of entities.
    /// 
    /// <para>
    /// Does not implement
    /// <list type="bullet">
    /// <item><see cref="IHasId{T}"/></item>
    /// <item><see cref="IHasGuidId"/></item>
    /// <item><see cref="IHasTenantFK"/></item>
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
    [DataContract]
    public abstract class UntenantedAuditedRecordStatedTimestampedNoneIdEntityBase
        : IHasTimestampRecordStateInRecordAuditability
    {
        /// <summary>
        /// Gets or sets the datastore concurrency check timestamp.
        /// <para>
        /// Note that this is filled in when persisted in the db --
        /// so it's usable to determine whether Record is New or not.
        /// </para>
        /// </summary>
        [ConcurrencyCheck]
        public virtual byte[] Timestamp { get; set; } = null!;

        /// <inheritdoc/>
        [NotMapped]
        public virtual RecordPersistenceState RecordState { get; set; }

        /// <inheritdoc/>
        [NotMapped]
        public virtual DateTime CreatedOnDateTimeUtc { get; set; }

        /// <inheritdoc/>
        [NotMapped]
        public virtual string CreatedByPrincipalId { get; set; } = string.Empty;

        /// <inheritdoc/>
        [NotMapped]
        public virtual DateTime LastModifiedOnDateTimeUtc { get; set; }

        /// <inheritdoc/>
        [NotMapped]
        public virtual string LastModifiedByPrincipalId { get; set; } = string.Empty;

        /// <inheritdoc/>
        [NotMapped]
        public virtual DateTime? StateChangedOnDateTimeUtc { get; set; }

        /// <inheritdoc/>
        [NotMapped]
        public virtual string? StateChangedByPrincipalId { get; set; }
    }
}
