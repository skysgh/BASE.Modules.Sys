using System;
using System.Linq.Expressions;
using App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Constants;
using App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Enums;
using App.Modules.Sys.Shared.Models;
using App.Modules.Sys.Shared.Models.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// Note how namespace does not match folders - this is intentional for easy discovery.
#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace App
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Contract-based EF Core EntityTypeBuilder extensions.
    /// These methods configure entity properties based on interface contracts,
    /// ensuring consistent schema definitions across all entities.
    /// </summary>
    public static class EntityTypeBuilderContractExtensions
    {
        private const bool PersistUUIDsAsString = true;



        /// <summary>
        /// Configure for <see cref="IHasPrincipalFK"/>.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasPrincipalFK<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order
            ) where TEntity : class, IHasPrincipalFK
        {
            builder.DefineGuid(
                x => x.PrincipalFK,
                ref order,
                isRequired: true,
                persistedAsString: PersistUUIDsAsString,
                optionalIndexType: IndexType.NonUnique);

            return builder;
        }

        /// <summary>
        /// Configure for <see cref="IHasParentFK"/>.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasParentFK<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order
            ) where TEntity : class, IHasParentFK
        {
            builder.DefineGuid(
                x => x.ParentFK,
                ref order,
                isRequired: false,
                persistedAsString: PersistUUIDsAsString,
                optionalIndexType: IndexType.NonUnique);

            return builder;
        }

        // NOTE: IHasOwnerFK uses a method GetOwnerFk(), not a property.
        // Configure owner FK explicitly using DefineGuid with the actual property name.

        /// <summary>
        /// Configure for <see cref="IHasDisplayHints"/> (both DisplayStyleHint and DisplayOrderHint).
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasDisplayHints<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order
            ) where TEntity : class, IHasDisplayHints
        {
            builder.DefineIHasDisplayStyleHint(ref order);
            builder.DefineIHasDisplayOrderHint(ref order);

            return builder;
        }

        /// <summary>
        /// Configure for <see cref="IHasDisplayStyleHintNullable"/>.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasDisplayStyleHint<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order
            ) where TEntity : class, IHasDisplayStyleHintNullable
        {
            builder.DefineString(
                x => x.DisplayStyleHint,
                ref order,
                isRequired: false,
                maxLength: DbSchemaFieldSizeConstants.DisplayHintLength,
                unicode: true,
                optionalIndexType: IndexType.None);

            return builder;
        }

        /// <summary>
        /// Configure for <see cref="IHasDisplayOrderHint"/>.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasDisplayOrderHint<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order
            ) where TEntity : class, IHasDisplayOrderHint
        {
            builder.DefineInt(
                x => x.DisplayOrderHint,
                ref order,
                isRequired: true,
                defaultValue: 0,
                optionalIndexType: IndexType.None);

            return builder;
        }

        /// <summary>
        /// Configure for <see cref="IHasImage"/>.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasImage<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order
            ) where TEntity : class, IHasImage
        {
            builder.DefineString(
                x => x.ImageUrl,
                ref order,
                isRequired: false,
                maxLength: DbSchemaFieldSizeConstants.UrlLength,
                unicode: false,
                optionalIndexType: IndexType.None);

            builder.DefineString(
                x => x.ImageStyle,
                ref order,
                isRequired: false,
                maxLength: DbSchemaFieldSizeConstants.DisplayHintLength,
                unicode: true,
                optionalIndexType: IndexType.None);

            return builder;
        }

        /// <summary>
        /// Configure for <see cref="IHasStartAndEndUtcNullable"/>.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasStartAndEndUtcNullable<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order,
            string? optionalStartIndexName = null,
            string? optionalEndIndexName = null
            ) where TEntity : class, IHasStartAndEndUtcNullable
        {
            builder.DefineDateTime(
                x => x.StartUtc,
                ref order,
                isRequired: false,
                optionalIndexType: IndexType.NonUnique,
                optionalIndexName: optionalStartIndexName);

            builder.DefineDateTime(
                x => x.EndUtc,
                ref order,
                isRequired: false,
                optionalIndexType: IndexType.NonUnique,
                optionalIndexName: optionalEndIndexName);

            return builder;
        }




        /// <summary>
        /// Configure for <see cref="IHasTagNullable"/>.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasTagNullable<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order,
            IndexType optionalIndexType = IndexType.None,
            string? optionalIndexName = null
            ) where TEntity : class, IHasTagNullable
        {
            builder.DefineString(
                x => x.Tag,
                ref order,
                isRequired: false,
                maxLength: DbSchemaFieldSizeConstants.TagLength,
                unicode: true,
                optionalIndexType: optionalIndexType,
                optionalIndexName: optionalIndexName);

            return builder;
        }

        /// <summary>
        /// Configure for <see cref="IHasTag"/>.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasTag<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order,
            IndexType optionalIndexType = IndexType.None,
            string? optionalIndexName = null
            ) where TEntity : class, IHasTag
        {
            builder.DefineString(
                x => x.Tag,
                ref order,
                isRequired: true,
                maxLength: DbSchemaFieldSizeConstants.TagLength,
                unicode: true,
                optionalIndexType: optionalIndexType,
                optionalIndexName: optionalIndexName);

            return builder;
        }

        /// <summary>
        /// Configure for <see cref="IHasTimestampRecordState"/> (both Timestamp and RecordState).
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasTimestampAndRecordState<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order
            ) where TEntity : class, IHasTimestampRecordState
        {
            builder.DefineIHasTimestamp(ref order);
            builder.DefineIHasRecordState(ref order);

            return builder;
        }

        /// <summary>
        /// Configure for <see cref="IHasTimestamp"/>.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasTimestamp<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order
            ) where TEntity : class, IHasTimestamp
        {
            builder.Property(x => x.Timestamp)
                .HasColumnOrder(order++)
                .IsConcurrencyToken()
                .IsRowVersion();

            return builder;
        }

        /// <summary>
        /// Configure for <see cref="IHasRecordState"/>.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasRecordState<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order,
            IndexType optionalIndexType = IndexType.NonUnique,
            string? optionalIndexName = null
            ) where TEntity : class, IHasRecordState
        {
            builder.DefineInt(
                x => (int)x.RecordState,
                ref order,
                isRequired: true,
                optionalIndexType: optionalIndexType,
                optionalIndexName: optionalIndexName);

            return builder;
        }



        // TODO: DefineIHasInRecordAuditability, DefineIHasCreatedOnDateTimeUtc, 
        // DefineIHasLastModifiedOnDateTimeUtc, DefineIHasLastStateChangedOnDateTimeUtc
        // methods are commented out until the interfaces are fully defined with all required properties.

        /// <summary>
        /// Configure for <see cref="IHasEnabled"/>.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasEnabled<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order,
            bool defaultValue = true,
            IndexType optionalIndexType = IndexType.NonUnique,
            string? optionalIndexName = null
            ) where TEntity : class, IHasEnabled
        {
            builder.DefineBool(
                x => x.Enabled,
                ref order,
                isRequired: true,
                defaultValue: defaultValue,
                optionalIndexType: optionalIndexType,
                optionalIndexName: optionalIndexName);

            return builder;
        }

        /// <summary>
        /// Configure for <see cref="IHasKey"/>.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasKey<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order,
            IndexType optionalIndexType = IndexType.Unique,
            string? optionalIndexName = null
            ) where TEntity : class, IHasKey
        {
            builder.DefineString(
                x => x.Key,
                ref order,
                isRequired: true,
                maxLength: DbSchemaFieldSizeConstants.KeyLength,
                unicode: true,
                optionalIndexType: optionalIndexType,
                optionalIndexName: optionalIndexName);

            return builder;
        }

        /// <summary>
        /// Configure for <see cref="IHasName"/>.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasName<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order,
            IndexType optionalIndexType = IndexType.Unique,
            string? optionalIndexName = null
            ) where TEntity : class, IHasName
        {
            builder.DefineString(
                x => x.Name,
                ref order,
                isRequired: true,
                maxLength: DbSchemaFieldSizeConstants.NameLength,
                unicode: true,
                optionalIndexType: optionalIndexType,
                optionalIndexName: optionalIndexName);

            return builder;
        }

        /// <summary>
        /// Configure for <see cref="IHasTitleAndDescription"/>.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasTitleAndDescription<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order,
            IndexType optionalTitleIndexType = IndexType.None,
            string? optionalTitleIndexName = null
            ) where TEntity : class, IHasTitleAndDescription
        {
            builder.DefineIHasTitle(ref order, optionalTitleIndexType, optionalTitleIndexName);
            builder.DefineIHasDescription(ref order);

            return builder;
        }

        /// <summary>
        /// Configure for <see cref="IHasTitle"/>.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasTitle<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order,
            IndexType optionalIndexType = IndexType.None,
            string? optionalIndexName = null
            ) where TEntity : class, IHasTitle
        {
            builder.DefineString(
                x => x.Title,
                ref order,
                isRequired: true,
                maxLength: DbSchemaFieldSizeConstants.TitleLength,
                unicode: true,
                optionalIndexType: optionalIndexType,
                optionalIndexName: optionalIndexName);

            return builder;
        }

        /// <summary>
        /// Configure for <see cref="IHasDescription"/>.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasDescription<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order,
            bool isRequired = false,
            IndexType optionalIndexType = IndexType.None,
            string? optionalIndexName = null
            ) where TEntity : class, IHasDescription
        {
            builder.DefineString(
                x => x.Description,
                ref order,
                isRequired: isRequired,
                maxLength: DbSchemaFieldSizeConstants.DescriptionLength,
                unicode: true,
                optionalIndexType: optionalIndexType,
                optionalIndexName: optionalIndexName);

            return builder;
        }

        #region Primary Key Contracts

        /// <summary>
        /// Configure for <see cref="IHasGuidId"/> - entity primary key.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineIHasGuidId<TEntity>(
            this EntityTypeBuilder<TEntity> builder,
            ref int order,
            bool addIndex = true,
            bool persistedAsString = PersistUUIDsAsString
            ) where TEntity : class, IHasGuidId
        {
            builder.HasKey(x => x.Id);

            if (persistedAsString)
            {
                builder.Property(x => x.Id)
                    .HasColumnOrder(order++)
                    .IsRequired()
                    .HasMaxLength(DbSchemaFieldSizeConstants.GuidStringLength)
                    .HasConversion<string>()
                    .ValueGeneratedNever();
            }
            else
            {
                builder.Property(x => x.Id)
                    .HasColumnOrder(order++)
                    .IsRequired()
                    .ValueGeneratedNever();
            }

            if (addIndex)
            {
                builder.HasIndex(x => x.Id)
                    .HasDatabaseName($"IX_{typeof(TEntity).Name}_Id")
                    .IsUnique();
            }

            return builder;
        }

        #endregion
    }
}
