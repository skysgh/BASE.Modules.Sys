using System;

namespace App.Modules.Sys.Shared.Models.Persistence
{
    /// <summary>
    /// Contract for entities that track when they were last modified.
    /// </summary>
    public interface IHasLastModifiedOnDateTimeUtc
    {
        /// <summary>
        /// Gets or sets the UTC DateTime when the record was last modified.
        /// </summary>
        DateTime LastModifiedOnDateTimeUtc { get; set; }
    }

    /// <summary>
    /// Contract for entities that track when their state was last changed.
    /// </summary>
    public interface IHasLastStateChangedOnDateTimeUtc
    {
        /// <summary>
        /// Gets or sets the UTC DateTime when the record state was last changed.
        /// </summary>
        DateTime? LastStateChangedOnDateTimeUtc { get; set; }
    }
}
