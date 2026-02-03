using App.Modules.Sys.Shared.ObjectMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace App.Modules.Sys.Application.Services.ObjectMapping
{
    /// <summary>
    /// Discovers ObjectMapBase implementations via reflection.
    /// Scans Application assemblies at startup to register mappings.
    /// </summary>
    public static class ObjectMapDiscoveryService
    {
        /// <summary>
        /// Discover all ObjectMapBase&lt;,&gt; implementations in assembly.
        /// </summary>
        /// <param name="assembly">Assembly to scan</param>
        /// <returns>List of mapper types found</returns>
        public static List<Type> DiscoverObjectMaps(Assembly assembly)
        {
            var results = new List<Type>();

            try
            {
                // Find all types inheriting from ObjectMapBase<,>
                var mappers = assembly.GetTypes()
                    .Where(t => t.IsClass && 
                               !t.IsAbstract &&
                               IsObjectMapBase(t));

                results.AddRange(mappers);
            }
            catch (ReflectionTypeLoadException ex)
            {
                // Log or handle loader exceptions
                Console.WriteLine($"Warning: Could not load all types from {assembly.GetName().Name}");
                foreach (var loaderEx in ex.LoaderExceptions)
                {
                    Console.WriteLine($"  - {loaderEx?.Message}");
                }
            }

            return results;
        }

        /// <summary>
        /// Check if type inherits from ObjectMapBase&lt;,&gt;
        /// </summary>
        private static bool IsObjectMapBase(Type type)
        {
            var baseType = type.BaseType;
            while (baseType != null)
            {
                if (baseType.IsGenericType &&
                    baseType.GetGenericTypeDefinition().Name == "ObjectMapBase`2")
                {
                    return true;
                }
                baseType = baseType.BaseType;
            }
            return false;
        }

        /// <summary>
        /// Get source and destination types from ObjectMapBase&lt;TFrom, TTo&gt;
        /// </summary>
        public static (Type From, Type To)? GetMappingTypes(Type mapperType)
        {
            var baseType = mapperType.BaseType;
            while (baseType != null)
            {
                if (baseType.IsGenericType &&
                    baseType.GetGenericTypeDefinition().Name == "ObjectMapBase`2")
                {
                    var args = baseType.GetGenericArguments();
                    return (args[0], args[1]);
                }
                baseType = baseType.BaseType;
            }
            return null;
        }
    }
}
