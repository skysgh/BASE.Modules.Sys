using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using App.Modules.Sys.Shared.Models;
using App.Modules.Sys.Shared.Models.Implementations;
using App.Modules.Sys.Shared.Lifecycles;
using App.Modules.Sys.Shared.Services;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace App
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Extension methods for discovering services via reflection.
    /// </summary>
    public static class AssemblyServiceDiscoveryExtensions
    {
        /// <summary>
        /// Discovers services with lifecycle marker interfaces in assembly.
        /// </summary>
        public static List<ServiceDescriptor> DiscoverServices(
            this Assembly assembly,
            StartupLog? log = null)
        {
            var descriptors = new List<ServiceDescriptor>();
            
            try
            {
                var types = assembly.GetExportedTypes()
                    .Where(t => t.IsClass && !t.IsAbstract);
                
                foreach (var implType in types)
                {
                    var lifetime = implType.DetermineServiceLifetime();
                    if (!lifetime.HasValue)
                    {
                        continue;
                    }
                    
                    // Get all interfaces except lifecycle markers
                    var interfaces = implType.GetInterfaces()
                        .Where(i => !i.IsLifecycleMarker());
                    
                    foreach (var iface in interfaces)
                    {
                        // Handle generic types - register as open generic
                        Type serviceType;
                        Type implementationType;
                        
                        if (implType.IsGenericTypeDefinition)
                        {
                            // Skip non-generic interfaces when implementation is generic
                            // (e.g., skip IAppLogger when implementing IAppLogger<T>)
                            if (!iface.IsGenericType)
                            {
                                continue;
                            }
                            
                            // Open generic: IAppLogger<> -> AppLogger<>
                            serviceType = iface.GetGenericTypeDefinition();
                            implementationType = implType;
                        }
                        else if (implType.IsGenericType && !implType.IsGenericTypeDefinition)
                        {
                            // Closed generic constructed from open generic - skip, will be resolved at runtime
                            continue;
                        }
                        else
                        {
                            // Non-generic type
                            serviceType = iface;
                            implementationType = implType;
                        }
                        
                        var descriptor = new ServiceDescriptor(serviceType, implementationType, lifetime.Value);
                        descriptors.Add(descriptor);
                        
                        log?.Services.Add(new ImplementationDetails
                        {
                            Interface = serviceType,
                            Implementation = implementationType,
                            Description = $"{lifetime.Value}: {serviceType.Name} → {implementationType.Name}"
                        });
                    }
                    
                    // Also register concrete type if has lifecycle but no interfaces
                    if (!interfaces.Any() && lifetime.HasValue)
                    {
                        if (!implType.IsGenericTypeDefinition)
                        {
                            descriptors.Add(new ServiceDescriptor(implType, implType, lifetime.Value));
                        }
                    }
                }
            }
            catch (ReflectionTypeLoadException)
            {
                // Log warning but continue - some types may not load
                log?.Notes.Add($"Warning: Could not load some types from {assembly.GetName().Name}");
            }
            
            return descriptors;
        }
        
        /// <summary>
        /// Determines the DI lifetime from lifecycle marker interfaces.
        /// </summary>
        public static ServiceLifetime? DetermineServiceLifetime(this Type type)
        {
            if (typeof(IHasSingletonLifecycle).IsAssignableFrom(type))
            {
                return ServiceLifetime.Singleton;
            }
            if (typeof(IHasScopedLifecycle).IsAssignableFrom(type))
            {
                return ServiceLifetime.Scoped;
            }
            if (typeof(IHasTransientLifecycle).IsAssignableFrom(type))
            {
                return ServiceLifetime.Transient;
            }
            
            return null; // Not a service
        }
        
        /// <summary>
        /// Checks if interface is a marker interface (should not be registered).
        /// Includes lifecycle markers AND service discovery markers.
        /// </summary>
        /// <remarks>
        /// Marker interfaces are used for:
        /// 1. Lifecycle determination (IHasSingletonLifecycle, IHasScopedLifecycle, IHasTransientLifecycle)
        /// 2. Service discovery (IHasService, IHasScopedService, IHasRegistryService, etc.)
        /// 
        /// These should NOT be registered as service types in DI - only the BUSINESS interfaces
        /// in the inheritance chain should be registered.
        /// 
        /// Example:
        /// ServiceA : IServiceB : IHasService : IHasSingletonLifecycle
        ///   ✓ Register: ServiceA → IServiceB
        ///   ✗ Skip: IHasService, IHasSingletonLifecycle, IHasLifecycle
        /// </remarks>
        public static bool IsLifecycleMarker(this Type interfaceType)
        {
            // Namespace-based exclusion (most robust)
            var ns = interfaceType.Namespace;
            if (ns != null)
            {
                // Exclude all interfaces in *.Shared.Lifecycles namespace
                if (ns.Contains(".Shared.Lifecycles", StringComparison.Ordinal))
                {
                    return true;
                }

                // Exclude marker interfaces in *.Shared.Services namespace
                // BUT NOT business interfaces that happen to be there
                if (ns.Contains(".Shared.Services", StringComparison.Ordinal))
                {
                    // Only exclude the marker interfaces by name
                    var name = interfaceType.Name;
                    if (name.StartsWith("IHas", StringComparison.Ordinal) && 
                        (name.Contains("Service", StringComparison.Ordinal) || 
                         name.Contains("Lifecycle", StringComparison.Ordinal) || 
                         name.Contains("Registry", StringComparison.Ordinal)))
                    {
                        return true;
                    }
                }
            }

            // Name-based exclusion (fallback for edge cases)
            var typeName = interfaceType.Name;
            
            // Lifecycle markers
            if (typeName.Contains("Lifecycle", StringComparison.Ordinal))
            {
                return true;
            }
            
            // Service discovery markers (IHasService, IHasScopedService, IHasRegistryService, etc.)
            if (typeName.StartsWith("IHas", StringComparison.Ordinal) && 
                typeName.EndsWith("Service", StringComparison.Ordinal) && 
                !typeName.Contains("Context", StringComparison.Ordinal))
            {
                // But NOT business services like IHasContextService - those are real services
                // Check if it's a known marker type
                return typeName == "IHasService" ||
                       typeName == "IHasScopedService" ||
                       typeName == "IHasRegistryService" ||
                       typeName == "IHasAppService" ||
                       typeName == "IHasAppCoreService" ||
                       typeName == "IHasAppInfrastructureService";
            }
            
            // Explicit type checks (belt and suspenders)
            if (interfaceType == typeof(IHasSingletonLifecycle) ||
                interfaceType == typeof(IHasScopedLifecycle) ||
                interfaceType == typeof(IHasTransientLifecycle) ||
                interfaceType == typeof(IHasLifecycle) ||
                interfaceType == typeof(IHasService) ||
                interfaceType == typeof(IHasScopedService))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Discovers cache objects (ICacheObject implementations) in assembly.
        /// Cache objects are always registered as singletons via their interface inheritance.
        /// </summary>
        public static List<ServiceDescriptor> DiscoverCacheObjects(
            this Assembly assembly,
            StartupLog? log = null)
        {
            var descriptors = new List<ServiceDescriptor>();
            
            try
            {
                var cacheObjectInterface = typeof(App.Modules.Sys.Shared.Services.Caching.ICacheObject);
                
                var types = assembly.GetExportedTypes()
                    .Where(t => t.IsClass 
                             && !t.IsAbstract 
                             && cacheObjectInterface.IsAssignableFrom(t));
                
                foreach (var implType in types)
                {
                    // Register as ICacheObject (singleton via interface inheritance)
                    var descriptor = new ServiceDescriptor(
                        cacheObjectInterface, 
                        implType, 
                        ServiceLifetime.Singleton);
                    
                    descriptors.Add(descriptor);
                    
                    log?.Services.Add(new ImplementationDetails
                    {
                        Interface = cacheObjectInterface,
                        Implementation = implType,
                        Description = $"CacheObject: {implType.Name}"
                    });
                }
            }
            catch (ReflectionTypeLoadException)
            {
                // Log warning but continue - some types may not load
                log?.Notes.Add($"Warning: Could not load some cache object types from {assembly.GetName().Name}");
            }
            
            return descriptors;
        }
    }
}
