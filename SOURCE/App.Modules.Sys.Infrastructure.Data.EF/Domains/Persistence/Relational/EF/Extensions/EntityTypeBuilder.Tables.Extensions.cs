using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Constants.DbSchemaFieldNameConstants;

#pragma warning disable CA2263 // Prefer generic overload when type is known
namespace App
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Extension methods for EF Core EntityTypeBuilder to create DRY, consistent schema definitions.
    /// 
    /// Design Notes:
    /// - Column ordering matters for performance (indexed/searched columns on left)
    /// - Methods that affect column order accept ref int order parameter
    /// - Temporal tables provide automatic audit history via SQL Server system versioning
    /// 
    /// Usage pattern in IEntityTypeConfiguration implementations:
    /// 1. Define table (with optional temporal support)
    /// 2. Define Id/Timestamp/Audit properties (in order)
    /// 3. Define FK constraints
    /// 4. Define custom properties
    /// 5. Define navigation properties (last)
    /// </summary>
    public static class EntityTypeBuilderTableExtensions
    {
        /// <summary>
        /// Define table name and schema.
        /// </summary>
        /// <remarks>
        /// Schema is required to avoid magic strings. Use your module's schema constant
        /// (e.g., ModuleConstants.DbSchemaKey).
        /// </remarks>
        /// <param name="builder">The entity type builder.</param>
        /// <param name="tableName">The table name.</param>
        /// <param name="schema">The schema name (required - use module constant).</param>
        public static EntityTypeBuilder<TEntity> DefineTable<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            string tableName,
            string schema) where TEntity : class
        {
            if (string.IsNullOrEmpty(tableName))
            {
                var metadata = builder.Metadata;
                tableName = metadata.GetTableName() ?? typeof(TEntity).Name.SimplePluralise();
            }

            ArgumentException.ThrowIfNullOrWhiteSpace(schema, nameof(schema));
            builder.ToTable(tableName, schema);
            return builder;
        }

        /// <summary>
        /// Enable SQL Server temporal table (system-versioned) for automatic audit history.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Can be chained after DefineTable to add temporal history to any table.
        /// SQL Server automatically tracks all changes with system-time columns.
        /// History is stored in a companion table.
        /// </para>
        /// <para>
        /// Use case: Adding temporal history to an existing table during migration.
        /// </para>
        /// <para>
        /// Requires SQL Server 2016+ or Azure SQL Database.
        /// </para>
        /// </remarks>
        /// <param name="builder">The entity type builder.</param>
        /// <param name="historyTableName">History table name. If null, uses "{TableName}History".</param>
        /// <param name="historySchema">History table schema. If null, uses same schema as main table.</param>
        public static EntityTypeBuilder<TEntity> EnableTemporalHistory<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            string? historyTableName = null,
            string? historySchema = null) where TEntity : class
        {
            // Get the current table name from metadata if history name not specified
            var metadata = builder.Metadata;
            var tableName = metadata.GetTableName() ?? typeof(TEntity).Name.SimplePluralise();

            var currentSchema = metadata.GetSchema();

            historyTableName ??= tableName + TemporalTableConstants.HistoryTableSuffix;
            historySchema ??= currentSchema;

            builder.ToTable(tableName, currentSchema, tableBuilder =>
            {
                tableBuilder.IsTemporal(temporalBuilder =>
                {
                    temporalBuilder.HasPeriodStart(TemporalTableConstants.SysStartTimeColumn);
                    temporalBuilder.HasPeriodEnd(TemporalTableConstants.SysEndTimeColumn);

                    if (!string.IsNullOrEmpty(historySchema))
                    {
                        temporalBuilder.UseHistoryTable(historyTableName, historySchema);
                    }
                    else
                    {
                        temporalBuilder.UseHistoryTable(historyTableName);
                    }
                });
            });

            return builder;
        }


    }
}
