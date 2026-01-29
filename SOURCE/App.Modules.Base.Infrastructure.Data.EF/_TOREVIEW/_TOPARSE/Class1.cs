
namespace App.Modules.DevSpikes.Infrastructure.Storage.Db.EF.Schemas
{
    using App.Base.Infrastructure.Storage.Db.EF.Schema.Definitions.Conventions.Implementations;
    using App.Base.Infrastructure.Storage.Db.EF.Schema.Management;
    using App.Modules.DevSpikes.Shared.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// A single DbContext Entity model map, 
    /// invoked via a Module's specific DbContext 
    /// when its 
    /// <see cref="DbContext.OnModelCreating(ModelBuilder)"/> 
    /// method invokes the implementation of
    /// <see cref="IModelBuilderOrchestrator"/>, which in turn
    /// finds implementations of 
    /// <see cref="IHasAppModuleDbContextModelBuilderInitializer"/>
    /// such as this one.
    /// </summary>
    public class AppModuleDbContextModelBuilderDefineExample :
        IHasAppModuleDbContextModelBuilderInitializer
    {
        /// <inheritdoc/>

        public void Define(ModelBuilder modelBuilder)
        {
            // Use the convention tool to setup the table name:
            new DefaultTableAndSchemaNamingConvention()
                .Define<Example>(modelBuilder);

            // Start the counter:
            var order = 1;

            // Use the convention
            // --------------------------------------------------
            // ID Properties:
            modelBuilder.Entity<Example>()
                .Property(x => x.Id)
                .HasColumnOrder(order++)
                .IsRequired();
            // --------------------------------------------------
            // FK Properties (for single entity Navigation Properties later):

            // --------------------------------------------------
            // Model Specific Properties:
            modelBuilder.Entity<Example>()
                .Property(x => x.Text)
                .HasColumnOrder(order++);
            // --------------------------------------------------
            // Single Entity Navigation Properties:

            // --------------------------------------------------
            // Collection Navigation Properties:

            // --------------------------------------------------
        }
    }
}