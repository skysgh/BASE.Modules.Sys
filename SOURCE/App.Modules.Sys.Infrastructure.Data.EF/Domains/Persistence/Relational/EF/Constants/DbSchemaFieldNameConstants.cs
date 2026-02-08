using App.Modules.Sys.Shared.Models.Persistence;

namespace App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Constants;

/// <summary>
/// Database schema name constants for this module.
/// Use these instead of magic strings when defining table schemas.
/// </summary>
public static class DbSchemaFieldNameConstants
{

    #region Table Configuration

    /// <summary>
    /// Constants for temporal table configuration.
    /// </summary>
    public static class TemporalTableConstants
    {
        /// <summary>System start time column name.</summary>
        public const string SysStartTimeColumn = "SysStartTime";

        /// <summary>System end time column name.</summary>
        public const string SysEndTimeColumn = "SysEndTime";

        /// <summary>Default suffix for history tables.</summary>
        public const string HistoryTableSuffix = "History";
    }

    /// <summary>
    /// Standard property names for auditability columns.
    /// <para>
    /// Aligned with <see cref="IHasInRecordAuditability"/> 
    /// </para>
    /// </summary>
    public static class Auditing
    {
        /// <summary>DateTime: Created timestamp (UTC).</summary>
        public const string CreatedOnDateTimeUtc = "CreatedOnDateTimeUtc";
        /// <summary>String: Principal ID who created the record.</summary>
        public const string CreatedByPrincipalId = "CreatedByPrincipalId";
        /// <summary>DateTime: Last modified timestamp (UTC).</summary>
        public const string LastModifiedOnDateTimeUtc = "LastModifiedOnDateTimeUtc";
        /// <summary>String: Principal ID who last modified the record.</summary>
        public const string LastModifiedByPrincipalId = "LastModifiedByPrincipalId";
        /// <summary>DateTime?: Soft delete timestamp (UTC).</summary>
        public const string StateChangedOnDateTimeUtc = "StateChangedOnUtc";
        /// <summary>String?: Principal ID who deleted the record.</summary>
        public const string StateChangedByPrincipalId = "StateChangedByPrincipalId";
        ///// <summary>DateTime: Simple created timestamp.</summary>
        //public const string CreatedAt = "CreatedAt";
        ///// <summary>DateTime?: Simple updated timestamp.</summary>
        //public const string UpdatedAt = "UpdatedAt";
        ///// <summary>Guid?: User who created.</summary>
        //public const string CreatedBy = "CreatedBy";
        ///// <summary>Guid?: User who last updated.</summary>
        //public const string UpdatedBy = "UpdatedBy";
    }

    /// <summary>
    /// Standard property names for common columns.
    /// </summary>
    public static class Common
    {
        /// <summary>Guid: Primary key.</summary>
        public const string Id = "Id";
        /// <summary>Guid: Primary key.</summary>
        public const string Fk = "Fk";
        /// <summary>byte[]: Row version for concurrency.</summary>
        public const string Timestamp = "Timestamp";
        /// <summary>int: Record state (active, deleted, etc.).</summary>
        public const string RecordState = "RecordState";
        /// <summary>bool: Is active flag.</summary>
        public const string IsActive = "IsActive";
        /// <summary>bool: Is default flag.</summary>
        public const string IsDefault = "IsDefault";
        /// <summary>int: Sort order.</summary>
        public const string SortOrder = "SortOrder";
        /// <summary>bool: Enabled flag (reference data).</summary>
        public const string Enabled = "Enabled";
/// <summary>
/// 
/// </summary>
        public const string Value = "Value";
        /// <summary>string: Title.</summary>
        public const string Title = "Title";
        /// <summary>string: Description.</summary>
        public const string Description = "Description";
        /// <summary>string: Name.</summary>
        public const string Name = "Name";

        /// <summary>String column: Key.</summary>
        public const string Key = "Key";

        ///// <summary>string: Native name (localized).</summary>
        //public const string NativeName = "NativeName";
        ///// <summary>string: Code (short, unique identifier).</summary>
        //public const string Code = "Code";


        /// <summary>
        /// An id 'en', or
        /// a full or partial, relative or absolute path, or
        /// class name.
        /// It is up to the presentation layer to know what to expect.
        /// </summary>
        public const string ImageIdOrPathOrClassHint = "ImageIdOrUrlOrClassHint";

        /// <summary>String column: Url.</summary>
        public const string Url = "Url";

        /// <summary>Int column: DisplayOrderHint.</summary>
        public const string DisplayOrderHint = "DisplayOrderHint";

        /// <summary>String column: DisplayStyleHint.</summary>
        public const string DisplayStyleHint = "DisplayStyleHint";

    #endregion
    }
}
