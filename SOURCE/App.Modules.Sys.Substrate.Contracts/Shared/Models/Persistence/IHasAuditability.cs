using System;

namespace App.Modules.Sys.Shared.Models.Persistence
{
    /// <summary>
    /// Contract for persistable system entities
    /// to include basic audit values in the record.
    /// <para>
    /// Note: <c>Created</c>/<c>Updated</c> properties always expect 
    /// a value, whereas state change properties are nullable.
    /// </para>
    /// </summary>
    public interface IHasInRecordAuditability :
        IHasCreatedOnDateTimeUtc,
        IHasLastModifiedOnDateTimeUtc
    {
        /// <summary>
        /// Gets or sets the principal id who created the record.
        /// </summary>
        string CreatedByPrincipalId { get; set; }

        /// <summary>
        /// Gets or sets the principal id who last modified the record.
        /// </summary>
        string LastModifiedByPrincipalId { get; set; }

        /// <summary>
        /// Gets or sets the date when record state changed (nullable for soft delete).
        /// </summary>
        DateTime? StateChangedOnDateTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the principal id who changed the state (nullable).
        /// </summary>
        string? StateChangedByPrincipalId { get; set; }
    }
}
