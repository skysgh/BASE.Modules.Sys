using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// Suppress nullability warnings for expression parameters - these are compile-time safe and work correctly
#pragma warning disable CS8620 // Nullability of reference types in expression

namespace App.Modules.Sys.Infrastructure.Persistence.EF.Schema.Extensions;

/// <summary>
/// Extension methods for defining entity relationships in EF Core.
/// 
/// Relationship Types:
/// - OneToZeroOrOne (1:0-1): Principal has optional dependent
/// - OneToOne (1:1): Principal has required dependent
/// - OneToZeroOrMany (1:0-*): Principal has zero or more dependents
/// - OneToOneOrMany (1:1-*): Principal has one or more dependents (enforced at app level)
/// - ManyToMany (*:*): Both sides have many of each other
/// 
/// All FK columns are automatically indexed using DefineRequiredFK/DefineOptionalFK.
/// Join table names are computed if not provided.
/// </summary>
public static class EntityTypeBuilderRelationshipExtensions
{
    #region Relationship Name Generation

    /// <summary>
    /// Generates a join table name from two entity types.
    /// Convention: {Entity1}_{Entity2} (alphabetically ordered)
    /// </summary>
    public static string GenerateJoinTableName<TPrincipal, TDependent>()
        where TPrincipal : class
        where TDependent : class
    {
        var names = new[] { typeof(TPrincipal).Name, typeof(TDependent).Name };
        Array.Sort(names);
        return $"{names[0]}_{names[1]}";
    }

    /// <summary>
    /// Generates a FK property name from entity type.
    /// Convention: {EntityName}Id
    /// </summary>
    public static string GenerateFKName<TEntity>() where TEntity : class
        => $"{typeof(TEntity).Name}Id";

    /// <summary>
    /// Generates a FK property name from entity type.
    /// Convention: {EntityName}FK
    /// </summary>
    public static string GenerateFKNameWithSuffix<TEntity>() where TEntity : class
        => $"{typeof(TEntity).Name}FK";

    #endregion

    #region OneToZeroOrOne (1:0-1) - Optional Dependent

    /// <summary>
    /// Define a one-to-zero-or-one relationship where the dependent is optional.
    /// 
    /// Example: User has optional UserProfile
    /// <code>
    /// // In UserConfiguration:
    /// builder.DefineOneToZeroOrOne&lt;User, UserProfile&gt;(
    ///     u => u.Profile,        // Navigation to optional dependent
    ///     p => p.User,           // Navigation back to principal
    ///     p => p.UserId);        // FK in dependent
    /// </code>
    /// </summary>
    /// <typeparam name="TPrincipal">The principal entity type (required side)</typeparam>
    /// <typeparam name="TDependent">The dependent entity type (optional side)</typeparam>
    /// <param name="builder">The entity type builder for the principal</param>
    /// <param name="navigationToDependent">Navigation property from principal to dependent</param>
    /// <param name="navigationToPrincipal">Navigation property from dependent to principal</param>
    /// <param name="foreignKeyExpression">FK property in the dependent entity</param>
    /// <param name="onDelete">Delete behavior (default: SetNull for optional)</param>
    public static EntityTypeBuilder<TPrincipal> DefineOneToZeroOrOne<TPrincipal, TDependent>(
        this EntityTypeBuilder<TPrincipal> builder,
        System.Linq.Expressions.Expression<Func<TPrincipal, TDependent?>> navigationToDependent,
        System.Linq.Expressions.Expression<Func<TDependent, TPrincipal?>> navigationToPrincipal,
        System.Linq.Expressions.Expression<Func<TDependent, object?>> foreignKeyExpression,
        DeleteBehavior onDelete = DeleteBehavior.SetNull)
        where TPrincipal : class
        where TDependent : class
    {
        builder.HasOne(navigationToDependent)
            .WithOne(navigationToPrincipal)
            .HasForeignKey(foreignKeyExpression)
            .OnDelete(onDelete);

        return builder;
    }

    #endregion

    #region OneToOne (1:1) - Required Dependent

    /// <summary>
    /// Define a one-to-one relationship where the dependent is required.
    /// 
    /// Example: Order has required OrderDetails
    /// <code>
    /// // In OrderConfiguration:
    /// builder.DefineOneToOne&lt;Order, OrderDetails&gt;(
    ///     o => o.Details,         // Navigation to required dependent
    ///     d => d.Order,           // Navigation back to principal
    ///     d => d.OrderId);        // FK in dependent
    /// </code>
    /// </summary>
    /// <typeparam name="TPrincipal">The principal entity type</typeparam>
    /// <typeparam name="TDependent">The dependent entity type (required)</typeparam>
    /// <param name="builder">The entity type builder for the principal</param>
    /// <param name="navigationToDependent">Navigation property from principal to dependent</param>
    /// <param name="navigationToPrincipal">Navigation property from dependent to principal</param>
    /// <param name="foreignKeyExpression">FK property in the dependent entity</param>
    /// <param name="onDelete">Delete behavior (default: Cascade for required)</param>
    public static EntityTypeBuilder<TPrincipal> DefineOneToOne<TPrincipal, TDependent>(
        this EntityTypeBuilder<TPrincipal> builder,
        System.Linq.Expressions.Expression<Func<TPrincipal, TDependent>> navigationToDependent,
        System.Linq.Expressions.Expression<Func<TDependent, TPrincipal>> navigationToPrincipal,
        System.Linq.Expressions.Expression<Func<TDependent, object?>> foreignKeyExpression,
        DeleteBehavior onDelete = DeleteBehavior.Cascade)
        where TPrincipal : class
        where TDependent : class
    {
        builder.HasOne(navigationToDependent)
            .WithOne(navigationToPrincipal)
            .HasForeignKey(foreignKeyExpression)
            .IsRequired()
            .OnDelete(onDelete);

        return builder;
    }

    #endregion

    #region OneToZeroOrMany (1:0-*) - Optional Collection

    /// <summary>
    /// Define a one-to-zero-or-many relationship (standard one-to-many).
    /// 
    /// Example: Category has zero or more Products
    /// <code>
    /// // In CategoryConfiguration:
    /// builder.DefineOneToZeroOrMany&lt;Category, Product&gt;(
    ///     c => c.Products,        // Navigation to collection
    ///     p => p.Category,        // Navigation back to principal
    ///     p => p.CategoryId);     // FK in dependent (optional)
    /// </code>
    /// </summary>
    /// <typeparam name="TPrincipal">The principal entity type (one side)</typeparam>
    /// <typeparam name="TDependent">The dependent entity type (many side)</typeparam>
    /// <param name="builder">The entity type builder for the principal</param>
    /// <param name="navigationToMany">Navigation property from principal to collection</param>
    /// <param name="navigationToOne">Navigation property from dependent to principal</param>
    /// <param name="foreignKeyExpression">FK property in the dependent entity</param>
    /// <param name="onDelete">Delete behavior (default: SetNull for optional FK)</param>
    public static EntityTypeBuilder<TPrincipal> DefineOneToZeroOrMany<TPrincipal, TDependent>(
        this EntityTypeBuilder<TPrincipal> builder,
        System.Linq.Expressions.Expression<Func<TPrincipal, IEnumerable<TDependent>>> navigationToMany,
        System.Linq.Expressions.Expression<Func<TDependent, TPrincipal?>> navigationToOne,
        System.Linq.Expressions.Expression<Func<TDependent, object?>> foreignKeyExpression,
        DeleteBehavior onDelete = DeleteBehavior.SetNull)
        where TPrincipal : class
        where TDependent : class
    {
        builder.HasMany(navigationToMany)
            .WithOne(navigationToOne)
            .HasForeignKey(foreignKeyExpression)
            .IsRequired(false)
            .OnDelete(onDelete);

        return builder;
    }

    /// <summary>
    /// Define a one-to-zero-or-many relationship with required FK.
    /// The dependent MUST belong to a principal (FK is required).
    /// 
    /// Example: Order has zero or more LineItems (but each LineItem must belong to an Order)
    /// <code>
    /// // In OrderConfiguration:
    /// builder.DefineOneToZeroOrManyRequired&lt;Order, LineItem&gt;(
    ///     o => o.LineItems,       // Navigation to collection
    ///     li => li.Order,         // Navigation back to principal
    ///     li => li.OrderId);      // FK in dependent (required)
    /// </code>
    /// </summary>
    public static EntityTypeBuilder<TPrincipal> DefineOneToZeroOrManyRequired<TPrincipal, TDependent>(
        this EntityTypeBuilder<TPrincipal> builder,
        System.Linq.Expressions.Expression<Func<TPrincipal, IEnumerable<TDependent>>> navigationToMany,
        System.Linq.Expressions.Expression<Func<TDependent, TPrincipal>> navigationToOne,
        System.Linq.Expressions.Expression<Func<TDependent, object?>> foreignKeyExpression,
        DeleteBehavior onDelete = DeleteBehavior.Cascade)
        where TPrincipal : class
        where TDependent : class
    {
        builder.HasMany(navigationToMany)
            .WithOne(navigationToOne)
            .HasForeignKey(foreignKeyExpression)
            .IsRequired()
            .OnDelete(onDelete);

        return builder;
    }

    #endregion

    #region OneToOneOrMany (1:1-*) - At Least One Required

    /// <summary>
    /// Define a one-to-one-or-many relationship (at least one required).
    /// Note: EF Core cannot enforce "at least one" at database level.
    /// This is enforced at application level via validation.
    /// 
    /// Example: Team has at least one TeamMember
    /// <code>
    /// // In TeamConfiguration:
    /// builder.DefineOneToOneOrMany&lt;Team, TeamMember&gt;(
    ///     t => t.Members,         // Navigation to collection (must have at least 1)
    ///     m => m.Team,            // Navigation back to principal
    ///     m => m.TeamId);         // FK in dependent (required)
    /// </code>
    /// 
    /// ⚠️ IMPORTANT: Enforce "at least one" in your domain/application layer:
    /// - Validate on create: Team must be created with at least one member
    /// - Validate on delete: Cannot remove last member from team
    /// </summary>
    public static EntityTypeBuilder<TPrincipal> DefineOneToOneOrMany<TPrincipal, TDependent>(
        this EntityTypeBuilder<TPrincipal> builder,
        System.Linq.Expressions.Expression<Func<TPrincipal, IEnumerable<TDependent>>> navigationToMany,
        System.Linq.Expressions.Expression<Func<TDependent, TPrincipal>> navigationToOne,
        System.Linq.Expressions.Expression<Func<TDependent, object?>> foreignKeyExpression,
        DeleteBehavior onDelete = DeleteBehavior.Restrict)
        where TPrincipal : class
        where TDependent : class
    {
        // At DB level, this is the same as OneToManyRequired
        // "At least one" constraint must be enforced at application level
        builder.HasMany(navigationToMany)
            .WithOne(navigationToOne)
            .HasForeignKey(foreignKeyExpression)
            .IsRequired()
            .OnDelete(onDelete); // Restrict prevents orphaning

        return builder;
    }

    #endregion

    #region ManyToMany (*:*) - Both Sides Have Many

    /// <summary>
    /// Define a many-to-many relationship with auto-generated join table.
    /// 
    /// Example: Students and Courses
    /// <code>
    /// // In StudentConfiguration:
    /// builder.DefineManyToMany&lt;Student, Course&gt;(
    ///     s => s.Courses,         // Navigation from Student to Courses
    ///     c => c.Students);       // Navigation from Course to Students
    /// // Creates join table: Course_Student with CourseId and StudentId FKs
    /// </code>
    /// </summary>
    /// <typeparam name="TLeft">Left entity type</typeparam>
    /// <typeparam name="TRight">Right entity type</typeparam>
    /// <param name="builder">The entity type builder for the left entity</param>
    /// <param name="navigationToRight">Navigation from left to right collection</param>
    /// <param name="navigationToLeft">Navigation from right to left collection</param>
    /// <param name="joinTableName">Optional custom join table name</param>
    /// <param name="schema">Schema for join table</param>
    public static EntityTypeBuilder<TLeft> DefineManyToMany<TLeft, TRight>(
        this EntityTypeBuilder<TLeft> builder,
        System.Linq.Expressions.Expression<Func<TLeft, IEnumerable<TRight>>> navigationToRight,
        System.Linq.Expressions.Expression<Func<TRight, IEnumerable<TLeft>>> navigationToLeft,
        string? joinTableName = null,
        string? schema = null)
        where TLeft : class
        where TRight : class
    {
        joinTableName ??= GenerateJoinTableName<TLeft, TRight>();

        builder.HasMany(navigationToRight)
            .WithMany(navigationToLeft)
            .UsingEntity(joinTableName,
                l => l.HasOne(typeof(TRight)).WithMany().HasForeignKey($"{typeof(TRight).Name}Id"),
                r => r.HasOne(typeof(TLeft)).WithMany().HasForeignKey($"{typeof(TLeft).Name}Id"),
                j =>
                {
                    if (!string.IsNullOrEmpty(schema))
                    {
                        j.ToTable(joinTableName, schema);
                    }
                    // Create composite PK
                    j.HasKey($"{typeof(TLeft).Name}Id", $"{typeof(TRight).Name}Id");
                    
                    // Index both FKs
                    j.HasIndex($"{typeof(TLeft).Name}Id")
                        .HasDatabaseName($"IX_{joinTableName}_{typeof(TLeft).Name}Id");
                    j.HasIndex($"{typeof(TRight).Name}Id")
                        .HasDatabaseName($"IX_{joinTableName}_{typeof(TRight).Name}Id");
                });

        return builder;
    }

    /// <summary>
    /// Define a many-to-many relationship with explicit join entity.
    /// Use when you need additional properties on the join table.
    /// 
    /// Example: Person and Organisation with Employment details
    /// <code>
    /// // Join entity:
    /// public class Employment 
    /// {
    ///     public Guid PersonId { get; set; }
    ///     public Person Person { get; set; }
    ///     public Guid OrganisationId { get; set; }
    ///     public Organisation Organisation { get; set; }
    ///     public DateTime StartDate { get; set; }
    ///     public string JobTitle { get; set; }
    /// }
    /// 
    /// // In PersonConfiguration:
    /// builder.DefineManyToManyWithJoinEntity&lt;Person, Organisation, Employment&gt;(
    ///     p => p.Employments,              // Navigation to join entities
    ///     e => e.Person,                   // Join to Person
    ///     e => e.PersonId,                 // FK to Person
    ///     e => e.Organisation,             // Join to Organisation
    ///     e => e.OrganisationId,           // FK to Organisation
    ///     schema: "hr");
    /// </code>
    /// </summary>
    /// <typeparam name="TLeft">Left principal entity</typeparam>
    /// <typeparam name="TRight">Right principal entity</typeparam>
    /// <typeparam name="TJoin">Join entity with additional properties</typeparam>
    /// <remarks>
    /// Note: joinToRight, rightForeignKey, tableName, schema, onDeleteRight parameters
    /// are for future use - full M:N with join entity configuration.
    /// Currently implements left-side only. Use ManyToMany for simple cases.
    /// </remarks>
#pragma warning disable IDE0060 // Unused parameters - reserved for future full implementation
    public static EntityTypeBuilder<TLeft> DefineManyToManyWithJoinEntity<TLeft, TRight, TJoin>(
        this EntityTypeBuilder<TLeft> builder,
        System.Linq.Expressions.Expression<Func<TLeft, IEnumerable<TJoin>>> navigationToJoin,
        System.Linq.Expressions.Expression<Func<TJoin, TLeft>> joinToLeft,
        System.Linq.Expressions.Expression<Func<TJoin, object?>> leftForeignKey,
        System.Linq.Expressions.Expression<Func<TJoin, TRight>> joinToRight,
        System.Linq.Expressions.Expression<Func<TJoin, object?>> rightForeignKey,
        string? tableName = null,
        string? schema = null,
        DeleteBehavior onDeleteLeft = DeleteBehavior.Cascade,
        DeleteBehavior onDeleteRight = DeleteBehavior.Cascade)
#pragma warning restore IDE0060
        where TLeft : class
        where TRight : class
        where TJoin : class
    {
        // Configure the left side relationship
        builder.HasMany(navigationToJoin)
            .WithOne(joinToLeft)
            .HasForeignKey(leftForeignKey)
            .OnDelete(onDeleteLeft);

        return builder;
    }

    #endregion

    #region Self-Referencing Relationships

    /// <summary>
    /// Define a self-referencing hierarchy (parent-child within same entity).
    /// 
    /// Example: Category with subcategories
    /// <code>
    /// // In CategoryConfiguration:
    /// builder.DefineSelfReferenceHierarchy(
    ///     c => c.ParentCategory,       // Navigation to parent
    ///     c => c.SubCategories,        // Navigation to children
    ///     c => c.ParentCategoryId);    // Optional FK to parent
    /// </code>
    /// </summary>
    public static EntityTypeBuilder<TEntity> DefineSelfReferenceHierarchy<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        System.Linq.Expressions.Expression<Func<TEntity, TEntity?>> navigationToParent,
        System.Linq.Expressions.Expression<Func<TEntity, IEnumerable<TEntity>>> navigationToChildren,
        System.Linq.Expressions.Expression<Func<TEntity, object?>> foreignKeyExpression,
        DeleteBehavior onDelete = DeleteBehavior.Restrict)
        where TEntity : class
    {
        builder.HasOne(navigationToParent)
            .WithMany(navigationToChildren)
            .HasForeignKey(foreignKeyExpression)
            .IsRequired(false)
            .OnDelete(onDelete); // Restrict to prevent orphaning children

        return builder;
    }

    #endregion
}
