using App.Base.Infrastructure.Factories;
using App.Base.Infrastructure.Storage.Db.EF.DbContexts.Implementations.Base;
using App.Modules.Core.Shared.Constants;
using Microsoft.EntityFrameworkCore;


namespace App.Modules.Core.Infrastructure._TOREVIEW.DbContexts.Implementations._TOREVIEW
{
    /// <summary>
    /// The Module specific DbContext (notice is has it's own Schema).
    /// <para>
    /// Inherits from the common <see cref="ModuleDbContextBase"/> 
    /// where <c>AppDbContextBase.SaveChanges</c>
    /// and <c>AppDbContextBase.SaveChangesAsync</c>
    /// intercept the save operation, 
    /// to clean up new/updated objects
    /// </para>
    /// <para>
    /// Also (and very importantly) the base class' static Constructor 
    /// ensures its migration capabilities work from the commandline.
    /// </para>
    /// </summary>
    /// <seealso cref="ModuleDbContextBase" />


    public class ModuleDbContext : ModuleDbContextBase
    {
        // I think the alias was for auto registration
        // in a named way.
        //[Alias(Constants.Db.AppCoreDbContextNames.Core)]

        /*
        /// <summary>
        /// Each DbContext manages its own 
        /// distinct schema in the database.
        /// <para>
        /// In most cases, its identical to the 
        /// MduleConstants short Key.
        /// </para>
        /// <para>
        /// Note: Except for the default schema, whose name
        /// is ''.
        /// </para>
        /// </summary>
        //public const string SchemaKey = ModuleConstants.DbSchemaKey;
        */

        /*
        /// <summary>
        /// Expost the Types/Tables specific to this DbContext
        /// </summary>
        public DbSet<NothingDefinedYet>? NothingDefinedYet { get; set; }
        */

        /// <summary>
        /// Constructor
        /// <para>
        /// Constructor invokes base with 
        /// Key ('AppCoreDbContext') used to find the 
        /// ConnectionString in web.config
        /// </para>
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ModuleDbContext() :
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            this(ModuleConstants.DbConnectionName)
        {
            // Note how the constructor calls 'this' 
            // instead of 'base' so that it gets to
            // the place where the schema is set.
        }



        /// <summary>
        /// Constructor
        /// </summary>
        /// <para>
        /// Note how its using a ServiceLocator in the middle
        /// to build a <c>DbContextOptions</c> to pass down.
        /// </para>
        /// <para>
        /// Note: Does not yet invoke 
        /// <see cref="DbContext.OnConfiguring(DbContextOptionsBuilder)"/>
        /// at this point.
        /// </para>
        /// <param name="connectionStringOrName"></param>
        public ModuleDbContext(string connectionStringOrName)
            : base(
                   ServiceLocator
                  .Get<DbContextOptionsBuilder>()
                  .UseSqlServer(connectionStringOrName)
                  .Options)
        {
            SchemaKey = ModuleConstants.DbSchemaKey;
        }


        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set the schema name first:
            modelBuilder.HasDefaultSchema(ModuleConstants.DbSchemaKey);

            // then call the base, which does some magic by
            // reflection:
            base.OnModelCreating(modelBuilder);
        }

    }
}

