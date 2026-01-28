//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Relational;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace App
//{
//    /// <summary>
//    /// Extension methods for PropertyBuilder to provide additional fluent API capabilities.
//    /// </summary>
//    /// <remarks>
//    /// <para>
//    /// These extensions enhance EF Core's fluent API with additional helper methods
//    /// for common property configuration scenarios.
//    /// </para>
//    /// <para>
//    /// <b>Note:</b> Column ordering is primarily a visual/organizational feature in the database.
//    /// It does not affect query performance but can improve readability of database schemas.
//    /// </para>
//    /// </remarks>
//    public static class PropertyBuilderExtensions
//    {
//        /// <summary>
//        /// Configures the order of the column as it appears in the database table.
//        /// </summary>
//        /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
//        /// <param name="propertyBuilder">The property builder instance.</param>
//        /// <param name="order">The zero-based order of the column in the table.</param>
//        /// <returns>The same property builder instance for fluent chaining.</returns>
//        /// <remarks>
//        /// <para>
//        /// <b>Column Order Conventions:</b>
//        /// - Primary keys typically start at order 0
//        /// - Tenant FK usually follows at order 1 (for multi-tenant scenarios)
//        /// - Audit fields (Created, Modified) typically come after business properties
//        /// - Business-specific properties fill the middle range
//        /// </para>
//        /// <para>
//        /// <b>Example Usage:</b>
//        /// <code>
//        /// modelBuilder.Entity&lt;User&gt;()
//        ///     .Property(u => u.Id).HasColumnOrder(0)
//        ///     .Property(u => u.TenantId).HasColumnOrder(1)
//        ///     .Property(u => u.Name).HasColumnOrder(2);
//        /// </code>
//        /// </para>
//        /// <para>
//        /// <b>Best Practice:</b>
//        /// Use the <c>ref int order</c> pattern from <see cref="ModelBuilderExtensions"/>
//        /// to automatically increment order values:
//        /// <code>
//        /// int order = 0;
//        /// modelBuilder.Entity&lt;User&gt;()
//        ///     .Property(u => u.Id).HasColumnOrder(order++)
//        ///     .Property(u => u.Name).HasColumnOrder(order++);
//        /// </code>
//        /// </para>
//        /// <para>
//        /// <b>Note:</b> This method wraps EF Core's native <c>HasColumnOrder</c> functionality
//        /// (reintroduced in EF Core 8.0). For earlier versions, this provides a consistent API
//        /// that can be adapted as needed.
//        /// </para>
//        /// </remarks>
//        /// <exception cref="ArgumentNullException">Thrown when <paramref name="propertyBuilder"/> is null.</exception>
//        public static PropertyBuilder<TProperty> HasColumnOrder<TProperty>(
//            this PropertyBuilder<TProperty> propertyBuilder,
//            int order)
//        {
//            ArgumentNullException.ThrowIfNull(propertyBuilder);

//            // Use EF Core's native HasColumnOrder (available in EF Core 8.0+)
//            // This sets the column order in the database schema
//            propertyBuilder.HasColumnOrder(order);

//            return propertyBuilder;
//        }

//        /// <summary>
//        /// Configures the order of the column as it appears in the database table (non-generic version).
//        /// </summary>
//        /// <param name="propertyBuilder">The property builder instance.</param>
//        /// <param name="order">The zero-based order of the column in the table.</param>
//        /// <returns>The same property builder instance for fluent chaining.</returns>
//        /// <remarks>
//        /// This is the non-generic version of <see cref="HasColumnOrder{TProperty}"/>.
//        /// Use this when working with non-generic PropertyBuilder instances.
//        /// </remarks>
//        /// <exception cref="ArgumentNullException">Thrown when <paramref name="propertyBuilder"/> is null.</exception>
//        public static PropertyBuilder HasColumnOrder(
//            this PropertyBuilder propertyBuilder,
//            int order)
//        {
//            ArgumentNullException.ThrowIfNull(propertyBuilder);

//            // Use EF Core's native HasColumnOrder
//            propertyBuilder.HasColumnOrder(order);

//            return propertyBuilder;
//        }

//        /// <summary>
//        /// Configures a property as required (NOT NULL) and returns the builder for chaining.
//        /// </summary>
//        /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
//        /// <param name="propertyBuilder">The property builder instance.</param>
//        /// <param name="required">Whether the property is required (default: true).</param>
//        /// <returns>The same property builder instance for fluent chaining.</returns>
//        /// <remarks>
//        /// Convenience method that wraps <see cref="PropertyBuilder.IsRequired"/> 
//        /// with a parameter for conditional configuration.
//        /// </remarks>
//        public static PropertyBuilder<TProperty> IsRequiredIf<TProperty>(
//            this PropertyBuilder<TProperty> propertyBuilder,
//            bool required = true)
//        {
//            ArgumentNullException.ThrowIfNull(propertyBuilder);

//            propertyBuilder.IsRequired(required);
//            return propertyBuilder;
//        }

//        /// <summary>
//        /// Configures the maximum length of string or binary property data.
//        /// </summary>
//        /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
//        /// <param name="propertyBuilder">The property builder instance.</param>
//        /// <param name="maxLength">The maximum length of the data. Use -1 for MAX.</param>
//        /// <returns>The same property builder instance for fluent chaining.</returns>
//        /// <remarks>
//        /// <para>
//        /// <b>Common Max Length Values:</b>
//        /// - 50: Short codes, keys
//        /// - 100: Names, titles
//        /// - 256: Email addresses, URLs
//        /// - 1000: Short descriptions
//        /// - 4000: Long descriptions
//        /// - -1: MAX (unlimited, use sparingly)
//        /// </para>
//        /// <para>
//        /// See <see cref="App.Modules.Base.Infrastructure.Data.EF.Constants.DbFieldConstants"/> 
//        /// for standard length constants used across the application.
//        /// </para>
//        /// </remarks>
//        public static PropertyBuilder<TProperty> HasMaxLengthIf<TProperty>(
//            this PropertyBuilder<TProperty> propertyBuilder,
//            int maxLength)
//        {
//            ArgumentNullException.ThrowIfNull(propertyBuilder);

//            if (maxLength > 0 || maxLength == -1)
//            {
//                propertyBuilder.HasMaxLength(maxLength);
//            }

//            return propertyBuilder;
//        }

//        /// <summary>
//        /// Configures a property with a default value.
//        /// </summary>
//        /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
//        /// <param name="propertyBuilder">The property builder instance.</param>
//        /// <param name="defaultValue">The default value for the property.</param>
//        /// <returns>The same property builder instance for fluent chaining.</returns>
//        /// <remarks>
//        /// <para>
//        /// The default value is used when inserting a row without specifying this property.
//        /// </para>
//        /// <para>
//        /// <b>Example:</b>
//        /// <code>
//        /// modelBuilder.Entity&lt;User&gt;()
//        ///     .Property(u => u.IsActive).HasDefaultValue(true)
//        ///     .Property(u => u.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
//        /// </code>
//        /// </para>
//        /// </remarks>
//        public static PropertyBuilder<TProperty> HasDefaultValue<TProperty>(
//            this PropertyBuilder<TProperty> propertyBuilder,
//            TProperty? defaultValue)
//        {
//            ArgumentNullException.ThrowIfNull(propertyBuilder);

//            if (defaultValue != null)
//            {
//                propertyBuilder.HasDefaultValue(defaultValue);
//            }

//            return propertyBuilder;
//        }

//        /// <summary>
//        /// Configures a computed column with the specified SQL expression.
//        /// </summary>
//        /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
//        /// <param name="propertyBuilder">The property builder instance.</param>
//        /// <param name="sql">The SQL expression that computes the column value.</param>
//        /// <param name="stored">Whether the computed value is physically stored in the database.</param>
//        /// <returns>The same property builder instance for fluent chaining.</returns>
//        /// <remarks>
//        /// <para>
//        /// <b>Computed Column Examples:</b>
//        /// <code>
//        /// // Calculated field
//        /// .Property(p => p.FullName)
//        ///     .HasComputedColumnSql("[FirstName] + ' ' + [LastName]", stored: false);
//        /// 
//        /// // Stored computed field (better for queries)
//        /// .Property(p => p.TotalPrice)
//        ///     .HasComputedColumnSql("[Quantity] * [UnitPrice]", stored: true);
//        /// </code>
//        /// </para>
//        /// <para>
//        /// <b>Stored vs Non-Stored:</b>
//        /// - stored: true → Value calculated once and stored (can be indexed)
//        /// - stored: false → Value calculated on every read (no storage overhead)
//        /// </para>
//        /// </remarks>
//        public static PropertyBuilder<TProperty> HasComputedColumn2<TProperty>(
//            this PropertyBuilder<TProperty> propertyBuilder,
//            string sql,
//            bool stored = false)
//        {
//            ArgumentNullException.ThrowIfNull(propertyBuilder);
//            ArgumentException.ThrowIfNullOrWhiteSpace(sql);

//            propertyBuilder.HasComputedColumnSql(sql, stored);
//            return propertyBuilder;
//        }

//        /// <summary>
//        /// Configures the database column name for the property.
//        /// </summary>
//        /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
//        /// <param name="propertyBuilder">The property builder instance.</param>
//        /// <param name="name">The name of the column in the database table.</param>
//        /// <returns>The same property builder instance for fluent chaining.</returns>
//        /// <remarks>
//        /// <para>
//        /// <b>When to Use:</b>
//        /// - Map a property to a different column name than the property name
//        /// - Work with legacy databases that use different naming conventions
//        /// - Follow database naming standards (e.g., snake_case, PascalCase)
//        /// - Avoid naming conflicts with reserved database keywords
//        /// </para>
//        /// <para>
//        /// <b>Example Usage:</b>
//        /// <code>
//        /// // Property named 'EmailAddress' maps to column 'email_address'
//        /// modelBuilder.Entity&lt;User&gt;()
//        ///     .Property(u => u.EmailAddress)
//        ///     .HasDatabaseName("email_address");
//        /// 
//        /// // Avoid reserved keyword
//        /// modelBuilder.Entity&lt;User&gt;()
//        ///     .Property(u => u.Order)
//        ///     .HasDatabaseName("UserOrder");
//        /// 
//        /// // Chain with other configurations
//        /// modelBuilder.Entity&lt;User&gt;()
//        ///     .Property(u => u.Name)
//        ///     .HasDatabaseName("user_name")
//        ///     .HasColumnOrder(2)
//        ///     .HasMaxLength(256)
//        ///     .IsRequired();
//        /// </code>
//        /// </para>
//        /// <para>
//        /// <b>Naming Conventions:</b>
//        /// - Use consistent naming across your schema
//        /// - Consider database-specific conventions (snake_case for PostgreSQL, PascalCase for SQL Server)
//        /// - Document any deviations from standard conventions
//        /// </para>
//        /// <para>
//        /// <b>Note:</b> This wraps EF Core's native <c>HasColumnName</c> method,
//        /// providing consistent API surface with other extension methods.
//        /// </para>
//        /// </remarks>
//        /// <exception cref="ArgumentNullException">Thrown when <paramref name="propertyBuilder"/> is null.</exception>
//        /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null or whitespace.</exception>
//        public static PropertyBuilder<TProperty> HasDatabaseName<TProperty>(
//            this PropertyBuilder<TProperty> propertyBuilder,
//            string name)
//        {
//            ArgumentNullException.ThrowIfNull(propertyBuilder);
//            ArgumentException.ThrowIfNullOrWhiteSpace(name);

//            // Use EF Core's native HasColumnName
//            propertyBuilder.HasColumnName(name);

//            return propertyBuilder;
//        }

//        /// <summary>
//        /// Configures the database column name for the property (non-generic version).
//        /// </summary>
//        /// <param name="propertyBuilder">The property builder instance.</param>
//        /// <param name="name">The name of the column in the database table.</param>
//        /// <returns>The same property builder instance for fluent chaining.</returns>
//        /// <remarks>
//        /// This is the non-generic version of <see cref="HasDatabaseName{TProperty}"/>.
//        /// Use this when working with non-generic PropertyBuilder instances.
//        /// </remarks>
//        /// <exception cref="ArgumentNullException">Thrown when <paramref name="propertyBuilder"/> is null.</exception>
//        /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null or whitespace.</exception>
//        public static PropertyBuilder HasDatabaseName(
//            this PropertyBuilder propertyBuilder,
//            string name)
//        {
//            ArgumentNullException.ThrowIfNull(propertyBuilder);
//            ArgumentException.ThrowIfNullOrWhiteSpace(name);

//            // Use EF Core's native HasColumnName
//            propertyBuilder.HasColumnName(name);

//            return propertyBuilder;
//        }
//    }
//}
