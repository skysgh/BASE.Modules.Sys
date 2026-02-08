//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Services
//{
//    using System;
//    using System.Linq;
//    using System.Linq.Expressions;
//    using App.Base.Shared.Models;
//    using App.Modules.Sys.Shared.Models.Persistence;

//    /// Specifies how objects being loaded into the context are merged with objects already in the context.
//    public enum MergeOption
//    {
//        Undefined = -1,

//        /// Objects already in the context are not loaded from the data source (default behavior).
//        AppendOnly = 0,

//        /// Objects are always loaded from the data source. Any property changes made
//        /// to objects in the object context are overwritten by the data source values.
//        OverwriteChanges = 1,

//        /// unmodified properties of objects in the object context are overwritten with server values.
//        PreserveChanges = 2,

//        /// Objects are maintained in a Detached state and are not tracked.
//        NoTracking = 3
//    }


//    // The contract for a generic repository service
//    public interface IRepositoryService : IHasAppCoreService
//    {
//        bool HasChanges(string contextKey);

//        // Determines if any elements with the specified condition exists (faster than Count).
//        bool Any<TModel>(string contextKey, Expression<Func<TModel, bool>> filterPredicate = null) where TModel : class;

//        int Count<TModel>(string contextKey, Expression<Func<TModel, bool>> filterPredicate = null)
//            where TModel : class;

//        // Prefer the use of GetByFilter
//        IQueryable<TModel> GetQueryableSet<TModel>(string contextKey) where TModel : class;

//        TModel GetSingle<TModel>(string contextKey, Expression<Func<TModel, bool>> filterPredicate,
//            MergeOption mergeOptions = MergeOption.Undefined) where TModel : class;

//        IQueryable<TModel> GetQueryableSingle<TModel>(string contextKey, Expression<Func<TModel, bool>> filterPredicate,
//            MergeOption mergeOptions = MergeOption.Undefined) where TModel : class;

//        IQueryable<TModel> GetByFilter<TModel>(string contextKey, Expression<Func<TModel, bool>> filterPredicate,
//            MergeOption mergeOptions = MergeOption.Undefined) where TModel : class;

//        /// Adds or updates a model which matches the given criteria. Used only for seeding.
//        /// IMPORTANT: Until Committed, most ORMs (eg: EF) will return unchanged entities in subsequent queries.
//        void AddOrUpdate<TModel>(string contextKey, Expression<Func<TModel, object>> identifierExpression,
//            params TModel[] models) where TModel : class;

//        /// Persists the given entity.
//        /// IMPORTANT: Until Committed, most ORMs (eg: EF) will return unchanged entities in subsequent queries.
//        void PersistOnCommit<TModel, TId>(string contextKey, TModel model) where TModel : class, IHasTimestamp;


//        /// Adds the given model to the datastore, when Commit is invoked.
//        /// IMPORTANT: Until Committed, most ORMs (eg: EF) will return unchanged entities in subsequent queries.
//        void AddOnCommit<TModel>(string contextKey, TModel model) where TModel : class;


//        /// Sets state to Modified. If untracked, Attachs first.
//        /// IMPORTANT: Until Committed, most ORMs (eg: EF) will return unchanged entities in subsequent queries.
//        void UpdateOnCommit<TModel>(string contextKey, TModel model) where TModel : class;

//        /// Removes entities from the datastore that match the given filter, when the active IUnitOfWork is Committed.
//        /// IMPORTANT: Until Committed, most ORMs (eg: EF) will return unchanged entities in subsequent queries.
//        void DeleteOnCommit<TModel, TId>(string contextKey, TId id) where TModel : class, IHasId<TId>, new();

//        void DeleteOnCommit<TModel>(string contextKey, TModel model) where TModel : class;

//        void DeleteOnCommit<TModel>(string contextKey, Expression<Func<TModel, bool>> filterPredicate)
//            where TModel : class;

//        bool IsAttached<TModel>(string contextKey, TModel model) where TModel : class;

//        /// Attaches untracked -- but already saved at some point -- entities. Prefer UpdateOnCommit.
//        /// IMPORTANT: Until Committed, most ORMs (eg: EF) will return unchanged entities in subsequent queries.
//        void AttachOnCommit<TModel>(string contextKey, TModel model) where TModel : class;

//        void Detach<TModel>(string contextKey, TModel model) where TModel : class;

//        bool IsNew<T>(T model) where T : IHasTimestamp;

//        /// <summary>
//        /// IT will save automatically be sure you want to do this!
//        /// </summary>
//        /// <param name="contextKey"></param>
//        void SaveChanges(string contextKey);
//    }
//}
