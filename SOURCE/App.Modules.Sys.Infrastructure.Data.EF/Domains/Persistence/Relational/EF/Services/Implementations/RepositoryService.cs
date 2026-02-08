//﻿using App.Base.Infrastructure.Initialization.DependencyResolution;

//namespace App.Base.Infrastructure.Services.Implementations
//{
//    using System;
//    using System.Data.Entity;
//    using System.Data.Entity.Migrations;
//    using System.Linq;
//    using System.Linq.Expressions;
//    using App.Base.Infrastructure.Constants;
//    using App.Base.Infrastructure.Constants.Db;
//    using App.Base.Shared.Models;

//    /// <summary>
//    ///     Implementation of the
//    ///     <see cref="IRepositoryService" />
//    ///     Infrastructure Service Contract
//    /// </summary>
//    /// <seealso cref="App.Base.Infrastructure.Services.IRepositoryService" />
//    public class RepositoryService : AppCoreServiceBase, IRepositoryService
//    {

//        /// <summary>
//        /// Initializes a new instance of the <see cref="RepositoryService"/> class.
//        /// </summary>
//        public RepositoryService()
//        {
//        }


//        /// <inheritdoc/>
//        public bool HasChanges(string contextKey)
//        {
//            return HasChanges(GetDbContext(contextKey));
//        }

//        /// <inheritdoc/>
//        protected bool HasChanges(DbContext context)
//        {
//            return context.ChangeTracker.HasChanges();
//        }

//        /// <inheritdoc/>
//        public bool Any<TModel>(string contextKey, Expression<Func<TModel, bool>> filter = null) where TModel : class
//        {
//            return filter != null ? GetDbSet<TModel>(contextKey).Any(filter) : GetDbSet<TModel>(contextKey).Any();
//        }

//        /// <inheritdoc/>
//        public int Count<TModel>(string contextKey, Expression<Func<TModel, bool>> filter = null) where TModel : class
//        {
//            return filter != null ? GetDbSet<TModel>(contextKey).Count(filter) : GetDbSet<TModel>(contextKey).Count();
//        }

//        /// <inheritdoc/>
//        public TModel GetSingle<TModel>(string contextKey, Expression<Func<TModel, bool>> filter,
//            MergeOption mergeOptions) where TModel : class
//        {
//            return GetDbSet<TModel>(contextKey).SingleOrDefault(filter);
//        }

//        /// <inheritdoc/>
//        public IQueryable<TModel> GetQueryableSet<TModel>(string contextKey) where TModel : class
//        {
//            return GetDbSet<TModel>(contextKey);
//        }

//        /// <inheritdoc/>
//        public IQueryable<TModel> GetQueryableSingle<TModel>(string contextKey, Expression<Func<TModel, bool>> filter,
//            MergeOption mergeOptions) where TModel : class
//        {
//            return GetByFilter(contextKey, filter, mergeOptions).Take(1);
//        }

//        /// <inheritdoc/>
//        public IQueryable<TModel> GetByFilter<TModel>(string contextKey, Expression<Func<TModel, bool>> filter,
//            MergeOption mergeOptions) where TModel : class
//        {
//            return GetDbSet<TModel>(contextKey).Where(filter);
//        }


//        /// <inheritdoc/>
//        public void AddOrUpdate<TModel>(string contextKey, Expression<Func<TModel, object>> identifierExpression,
//            params TModel[] models) where TModel : class
//        {
//            GetDbSet<TModel>(contextKey).AddOrUpdate(identifierExpression, models);
//        }

//        /// <inheritdoc/>
//        public void PersistOnCommit<TModel, TId>(string contextKey, TModel model) where TModel : class, IHasTimestamp
//        {
//            if (IsNew(model))
//            {
//                AddOnCommit(contextKey, model);
//            }
//            else
//            {
//                UpdateOnCommit(contextKey, model);
//            }
//        }

//        /// <inheritdoc/>
//        public void AddOnCommit<TModel>(string contextKey, TModel model) where TModel : class
//        {
//            var debset = GetDbSet<TModel>(contextKey);
//            debset.Add(model);
//        }

//        /// <inheritdoc/>
//        public void UpdateOnCommit<TModel>(string contextKey, TModel model) where TModel : class
//        {
//            var dbEntityEntry = GetDbContext(contextKey).Entry(model);
//            if (((int) dbEntityEntry.State).BitIsSet((int) EntityState.Detached))
//            {
//                GetDbSet<TModel>(contextKey).Attach(model);
//            }
//            //Is this really needed?
//            if (((int) dbEntityEntry.State).BitIsNotSet((int) EntityState.Modified))
//            {
//                dbEntityEntry.State |= EntityState.Modified;
//            }
//        }



//        /// <inheritdoc/>
//        public void DeleteOnCommit<TModel, TId>(string contextKey, TId id) where TModel : class, IHasId<TId>, new()
//        {
//            var model = Activator.CreateInstance<TModel>();
//            model.Id = id;
//            var entityEntry = GetDbContext(contextKey).Entry(model);
//            entityEntry.State = EntityState.Deleted;
//        }

//        /// <inheritdoc/>
//        public void DeleteOnCommit<TModel>(string contextKey, TModel model) where TModel : class
//        {
//            try
//            {
//                //We reset original values so that Auditing (see AuditableChangesStrategy) records Info *before* changes...
//                var entityEntry = GetDbContext(contextKey).Entry(model);
//                entityEntry.CurrentValues.SetValues(entityEntry.OriginalValues);
//            }
//            catch
//            {
//            }
//            GetDbSet<TModel>(contextKey).Remove(model);
//        }

//        /// <inheritdoc/>
//        public void DeleteOnCommit<TModel>(string contextKey, Expression<Func<TModel, bool>> predicate)
//            where TModel : class
//        {
//            var models = GetByFilter(contextKey, predicate, MergeOption.Undefined);

//            foreach (var entity in models)
//            {
//                DeleteOnCommit(contextKey, entity);
//            }
//        }

//        /// <inheritdoc/>
//        public bool IsAttached<TModel>(string contextKey, TModel model) where TModel : class
//        {
//            var dbEntityEntry = GetDbContext(contextKey).Entry(model);
//            {
//                return !((int) dbEntityEntry.State).BitIsSet((int) EntityState.Detached);
//            }
//        }

//        /// <inheritdoc/>
//        public void AttachOnCommit<TModel>(string contextKey, TModel model) where TModel : class
//        {
//            if (GetDbContext(contextKey).Entry(model).State != EntityState.Added) // since it's added dont' updated
//            {
//                GetDbSet<TModel>(contextKey).Attach(model);
//                GetDbContext(contextKey).Entry(model).State = EntityState.Modified;
//            }
//        }


//        /// <inheritdoc/>
//        public void Detach<TModel>(string contextKey, TModel model) where TModel : class
//        {
//            GetDbContext(contextKey).Entry(model).State = EntityState.Detached;
//        }


//        /// <inheritdoc/>
//        public bool IsNew<T>(T model) where T : IHasTimestamp
//        {
//            IHasTimestamp timestampped = model;
//            return timestampped != null && (timestampped.Timestamp == null ? true : false);
//        }

//        /// <inheritdoc/>
//        public void SaveChanges(string contextKey)
//        {
//            GetDbContext(contextKey).SaveChanges();
//        }

//        protected virtual DbContext GetDbContext(string contextKey)
//        {
//            if (string.IsNullOrWhiteSpace(contextKey))
//            {
//                contextKey = AppCoreDbContextNames.Core;
//            }
//            return AppDependencyLocator.Current.GetInstance<DbContext>(contextKey);
//        }

//        protected DbSet<T> GetDbSet<T>(string contextKey) where T : class
//        {
//            return GetDbContext(contextKey).Set<T>();
//        }
//    }
//}
