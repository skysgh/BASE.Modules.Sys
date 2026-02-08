using System;
using System.Linq.Expressions;
using App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Constants;
using App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace App
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Primitive property extension methods for EF Core EntityTypeBuilder.
    /// These are the building blocks used by contract-based extensions.
    /// 
    /// Design: All methods use named parameters for clarity and accept ref int order
    /// for consistent column ordering.
    /// </summary>
    public static class EntityTypeBuilderExtensions
    {
        #region String Properties

        /// <summary>
        /// Define a string property with expression-based property selection.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineString<TEntity, TProperty>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, TProperty>> propertyExpression,
            ref int order,
            bool isRequired = true,
            int maxLength = DbSchemaFieldSizeConstants.StringDefaultLength,
            bool unicode = true,
            IndexType optionalIndexType = IndexType.None,
            string? optionalIndexName = null)
            where TEntity : class
        {
            var propertyBuilder = builder.Property(propertyExpression)
                .HasColumnOrder(order++)
                .IsRequired(isRequired)
                .HasMaxLength(maxLength)
                .IsUnicode(unicode);

            if (optionalIndexType != IndexType.None)
            {
                string propertyName = GetPropertyName(propertyExpression);
                builder.HasIndex(propertyName)
                    .HasDatabaseName(optionalIndexName ?? $"IX_{typeof(TEntity).Name}_{propertyName}")
                    .IsUnique(optionalIndexType == IndexType.Unique);
            }

            return builder;
        }

        #endregion

        #region Boolean Properties

        /// <summary>
        /// Define a boolean property with expression-based property selection.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineBool<TEntity, TProperty>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, TProperty>> propertyExpression,
            ref int order,
            bool isRequired = true,
            bool? defaultValue = null,
            IndexType optionalIndexType = IndexType.None,
            string? optionalIndexName = null)
            where TEntity : class
        {
            var propertyBuilder = builder.Property(propertyExpression)
                .HasColumnOrder(order++)
                .IsRequired(isRequired);

            if (defaultValue.HasValue)
            {
                propertyBuilder.HasDefaultValue(defaultValue.Value);
            }

            if (optionalIndexType != IndexType.None)
            {
                string propertyName = GetPropertyName(propertyExpression);
                builder.HasIndex(propertyName)
                    .HasDatabaseName(optionalIndexName ?? $"IX_{typeof(TEntity).Name}_{propertyName}")
                    .IsUnique(optionalIndexType == IndexType.Unique);
            }

            return builder;
        }

        #endregion

        #region Integer Properties

        /// <summary>
        /// Define an integer property with expression-based property selection.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineInt<TEntity, TProperty>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, TProperty>> propertyExpression,
            ref int order,
            bool isRequired = true,
            int? defaultValue = null,
            IndexType optionalIndexType = IndexType.None,
            string? optionalIndexName = null)
            where TEntity : class
        {
            var propertyBuilder = builder.Property(propertyExpression)
                .HasColumnOrder(order++)
                .IsRequired(isRequired);

            if (defaultValue.HasValue)
            {
                propertyBuilder.HasDefaultValue(defaultValue.Value);
            }

            if (optionalIndexType != IndexType.None)
            {
                string propertyName = GetPropertyName(propertyExpression);
                builder.HasIndex(propertyName)
                    .HasDatabaseName(optionalIndexName ?? $"IX_{typeof(TEntity).Name}_{propertyName}")
                    .IsUnique(optionalIndexType == IndexType.Unique);
            }

            return builder;
        }

        #endregion

        #region DateTime Properties

        /// <summary>
        /// Define a DateTime property with expression-based property selection.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineDateTime<TEntity, TProperty>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, TProperty>> propertyExpression,
            ref int order,
            bool isRequired = true,
            DateTime? defaultValue = null,
            IndexType optionalIndexType = IndexType.None,
            string? optionalIndexName = null)
            where TEntity : class
        {
            var propertyBuilder = builder.Property(propertyExpression)
                .HasColumnOrder(order++)
                .IsRequired(isRequired);

            if (defaultValue.HasValue)
            {
                propertyBuilder.HasDefaultValue(defaultValue.Value);
            }

            if (optionalIndexType != IndexType.None)
            {
                string propertyName = GetPropertyName(propertyExpression);
                builder.HasIndex(propertyName)
                    .HasDatabaseName(optionalIndexName ?? $"IX_{typeof(TEntity).Name}_{propertyName}")
                    .IsUnique(optionalIndexType == IndexType.Unique);
            }

            return builder;
        }

        #endregion

        #region Guid Properties

        /// <summary>
        /// Define a Guid property with expression-based property selection.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefineGuid<TEntity, TProperty>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, TProperty>> propertyExpression,
            ref int order,
            bool isRequired = true,
            bool persistedAsString = true,
            Guid? defaultValue = null,
            IndexType optionalIndexType = IndexType.None,
            string? optionalIndexName = null)
            where TEntity : class
        {
            string propertyName = GetPropertyName(propertyExpression);

            if (persistedAsString)
            {
#pragma warning disable CA2263 // Using non-generic Property() to force string type conversion
                var propertyBuilder = builder.Property(typeof(string), propertyName)
                    .HasColumnOrder(order++)
                    .IsRequired(isRequired)
                    .HasMaxLength(DbSchemaFieldSizeConstants.GuidStringLength);
#pragma warning restore CA2263

                if (defaultValue.HasValue)
                {
                    propertyBuilder.HasDefaultValue(defaultValue.Value.ToString());
                }
            }
            else
            {
                var propertyBuilder = builder.Property(propertyExpression)
                    .HasColumnOrder(order++)
                    .IsRequired(isRequired);

                if (defaultValue.HasValue)
                {
                    propertyBuilder.HasDefaultValue(defaultValue.Value);
                }
            }

            if (optionalIndexType != IndexType.None)
            {
                builder.HasIndex(propertyName)
                    .HasDatabaseName(optionalIndexName ?? $"IX_{typeof(TEntity).Name}_{propertyName}")
                    .IsUnique(optionalIndexType == IndexType.Unique);
            }

            return builder;
        }

        #endregion


        #region Property Index

        /// <summary>
        /// Define an index on a property.
        /// </summary>
        public static EntityTypeBuilder<TEntity> DefinePropertyIndex<TEntity, TProperty>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, TProperty>> propertyExpression,
            IndexType indexType = IndexType.Unique,
            string? optionalIndexName = null)
            where TEntity : class
        {
            if (indexType != IndexType.None)
            {
                string propertyName = GetPropertyName(propertyExpression);

                builder.HasIndex(propertyName)
                    .HasDatabaseName(optionalIndexName ?? $"IX_{typeof(TEntity).Name}_{propertyName}")
                    .IsUnique(indexType == IndexType.Unique);
            }

            return builder;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Extract property name from expression.
        /// </summary>
        private static string GetPropertyName<TEntity, TProperty>(
            Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            if (propertyExpression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }
            
            if (propertyExpression.Body is UnaryExpression unaryExpression 
                && unaryExpression.Operand is MemberExpression innerMember)
            {
                return innerMember.Member.Name;
            }

            throw new ArgumentException("Expression must be a member access expression", nameof(propertyExpression));
        }

        #endregion
    }
}
