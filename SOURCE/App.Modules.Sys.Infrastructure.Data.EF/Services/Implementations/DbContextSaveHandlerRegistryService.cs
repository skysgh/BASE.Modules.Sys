using App.Modules.Sys.Infrastructure.Data.EF.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace App.Modules.Sys.Infrastructure.Data.EF.Services.Implementations
{
    /// <summary>
    /// Central registry managing DbContext pre-commit processing strategies.
    /// Handles discovery, deduplication, and ordering.
    /// </summary>
    public class DbContextSaveHandlerRegistryService : IDbContextSaveHandlerRegistryService
    {
        private readonly List<IDbCommitPreCommitProcessingStrategy> _handlers = new();
        private readonly HashSet<Type> _registeredTypes = new();
        private readonly object _lock = new();



        /// <inheritdoc/>
        public void DiscoverAndRegister(Assembly assembly)
        {
            var handlerTypes = assembly.GetTypes()
                .Where(t => t.IsClass &&
                           !t.IsAbstract &&
                           typeof(IDbCommitPreCommitProcessingStrategy).IsAssignableFrom(t));

            lock (_lock)
            {
                foreach (var handlerType in handlerTypes)
                {
                    // Deduplicate by type
                    if (_registeredTypes.Contains(handlerType))
                    {
                        continue;
                    }

                    try
                    {
                        var handler = (IDbCommitPreCommitProcessingStrategy)Activator.CreateInstance(handlerType)!;
                        _handlers.Add(handler);
                        _registeredTypes.Add(handlerType);
                    }
                    catch (Exception)
                    {
                        // Ignore handlers that can't be instantiated
                    }
                }
            }
        }

        /// <inheritdoc/>
        public IDbCommitPreCommitProcessingStrategy[] GetOrderedHandlers()
        {
            lock (_lock)
            {
                return _handlers.OrderBy(h => h.Order).ToArray();
            }
        }

        /// <inheritdoc/>
        public int HandlerCount => _handlers.Count;
    }
}