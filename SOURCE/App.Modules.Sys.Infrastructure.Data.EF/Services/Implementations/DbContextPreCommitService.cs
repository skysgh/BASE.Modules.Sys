using App.Base.Infrastructure.Services;
using App.Modules.Sys.Infrastructure.Data.EF.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace App.Modules.Sys.Infrastructure.Data.EF.Services.Implementations
{
    //using App.Modules.Sys.Infrastructure.Db.DbContextFactories;
    //using App.Modules.Sys.Infrastructure.Db.DbContextFactories.Implementations;
    /// <summary>
    ///     Implementation of the
    ///     <see cref="IDbContextPreCommitService" />
    ///     Infrastructure Service Contract
    /// to pre-process all new/updated/modified entities
    /// belonging to a specific DbContext, before 
    /// they are saved.
    /// <para>
    /// This service implementation is invoked because
    /// the various DbContext implementations (eg: AppDbContext)
    /// override their SaveChanges method to do so
    /// TODO: currently it's not automatically handled from the IUnitOfWorkService implementation.
    /// </para>
    /// </summary>
    /// <seealso cref="IDbContextPreCommitService" />
    public class DbContextPreCommitService : IDbContextPreCommitService
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public DbContextPreCommitService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public void Initialize()
        {
            // No-op: Registry handles everything
        }

        /// <inheritdoc/>
        public void PreProcess(DbContext dbContext)
        {
            // Get handlers from registry (single source of truth)
            var registry = _serviceProvider.GetService<IDbContextSaveHandlerRegistryService>();
            if (registry == null)
            {
                return; // No registry in migrations
            }

            // Execute in priority order (registry returns ordered)
            foreach (var processor in registry.GetOrderedHandlers())
            {
                if (processor.Enabled)
                {
                    processor.Process(dbContext);
                }
            }
        }
    }
}

