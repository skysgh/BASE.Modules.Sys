using System;

namespace App.Modules.Sys.Shared.ObjectMaps
{
    /// <summary>
    /// Base class for object mapping definitions.
    /// Provides registration metadata without coupling to specific mapping library.
    /// </summary>
    /// <typeparam name="TFrom">Source type</typeparam>
    /// <typeparam name="TTo">Destination type</typeparam>
    /// <remarks>
    /// Design principles:
    /// - Pure data transformation (no business logic)
    /// - No dependency injection (stateless)
    /// - Security/filtering applied later at API boundary
    /// - Discovered via reflection at startup
    /// 
    /// Two usage patterns:
    /// 1. Pure convention (90%): Empty class - properties auto-map by name
    /// 2. Custom mapping (10%): Override ConfigureMapping() with fluent builder
    /// </remarks>
    public abstract class ObjectMapBase<TFrom, TTo> : IObjectMap<TFrom, TTo>
    {
        private MapBuilder<TFrom, TTo>? _builder;

        /// <inheritdoc/>
        public Type From { get; set; } = typeof(TFrom);

        /// <inheritdoc/>
        public Type To { get; set; } = typeof(TTo);

        /// <summary>
        /// Override to configure custom mappings.
        /// Default: Convention-based mapping (properties with same names).
        /// </summary>
        /// <remarks>
        /// Use fluent builder for full IntelliSense:
        /// <code>
        /// protected override void ConfigureMapping()
        /// {
        ///     CreateMap()
        ///         .Ignore(dest => dest.CalculatedField)
        ///         .MapFrom(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");
        /// }
        /// </code>
        /// </remarks>
        protected virtual void ConfigureMapping()
        {
            // Default: Empty - use convention-based mapping
        }

        /// <summary>
        /// Create fluent mapping builder with full IntelliSense.
        /// </summary>
        /// <returns>Builder for configuring mappings</returns>
        protected MapBuilder<TFrom, TTo> CreateMap()
        {
            _builder ??= new MapBuilder<TFrom, TTo>();
            return _builder;
        }

        /// <summary>
        /// Get configured mapping rules.
        /// Called by IObjectMappingService during registration.
        /// </summary>
        /// <returns>Mapping builder or null for convention</returns>
        public MapBuilder<TFrom, TTo>? GetMappingConfiguration()
        {
            if (_builder == null)
            {
                // Trigger configuration
                ConfigureMapping();
            }
            return _builder;
        }

        /// <summary>
        /// Legacy Configure method for backward compatibility.
        /// Prefer ConfigureMapping() with fluent builder instead.
        /// </summary>
        [Obsolete("Use ConfigureMapping() with CreateMap() fluent builder instead")]
        public virtual void Configure(dynamic config)
        {
            // Backward compatibility - do nothing
        }
    }
}

