namespace App.Modules.Sys.Shared.Models.Persistence
{
    /// <summary>
    /// Contract for entities that track when they were created.
    /// <para>
    /// See <see cref="IHasInRecordAuditability"/>
    /// </para>
    /// <para>
    /// There is no <c>IHasDateTimeCreatedUtcNullable</c>
    /// as new records always information as to when 
    /// it was created (as does Updated - but Deleted 
    /// must of course be Nullable).
    /// </para>
    /// <para>
    /// TODO: Not clear why there is a contract for only 
    /// a single aspect of what's covered by
    /// <see cref="IHasInRecordAuditability"/>
    /// (although its not named the same...)
    /// </para>
    /// </summary>
    public interface IHasCreatedOnDateTimeUtc
    {
        /// <summary>
        ///     Gets or sets the UTC DateTime created on.
        /// </summary>
        DateTime CreatedOnDateTimeUtc { get; set; }


    }
}