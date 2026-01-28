using App.Base.Infrastructure.Services;
using App.Modules.Base.Infrastructure.Data.EF.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace App.Modules.Base.Infrastructure.Data.EF.Services.Implementations
{
    //using App.Modules.Base.Infrastructure.Db.DbContextFactories;
    //using App.Modules.Base.Infrastructure.Db.DbContextFactories.Implementations;
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
        private IDbCommitPreCommitProcessingStrategy[] _processors;

        /// <summary>
        /// Constructor
        /// </summary>
        public DbContextPreCommitService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _processors = [];
            Initialize();

        }
        /// <summary>
        /// Constructor
        /// </summary>
        public DbContextPreCommitService(IServiceProvider serviceProvider, IDbCommitPreCommitProcessingStrategy[] processors)
        {
            _serviceProvider = serviceProvider;
            _processors = [];
            Initialize();
        }

        /// <inheritdoc/>
        public void Initialize()
        {
            _processors =
                _serviceProvider
                    .GetServices<IDbCommitPreCommitProcessingStrategy>()
                    .ToArray();
        }

        /// <summary>
        /// Pass all entities belonging to the specified DbContext
        /// through all implementations of 
        /// <see cref="IDbCommitPreCommitProcessingStrategy"/>
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public void PreProcess(DbContext dbContext)
        {
            _processors.ForEach(x => x.Process(dbContext));
        }
    }
}

