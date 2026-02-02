using App.Modules.Sys.Shared.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace App
{
    /// <summary>
    /// Extension methods for discovering module assemblies.
    /// </summary>
    public static class AssemblyHandlerDiscoveryExtensions
    {
        /// <summary>
        /// Extension methods for discovering of 
        /// of implimentations of IDbContextSaveHandler.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static List<Type> DiscoverDbContextHandlers(this Assembly assembly, StartupLog? log = null)
        {
            var results = new List<Type>();
            try
            {
                var handlerTypes = assembly.GetTypes()
                        .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
                        .Any(i => i.Name.Contains("IDbContextSaveHandler")));
                results.AddRange(handlerTypes);
                foreach (var h in handlerTypes) { log?.Notes.Add($"    Handler: {h.Name}"); }
            }
            catch (ReflectionTypeLoadException) { log?.Notes.Add($"Warning: Could not load handlers from {assembly.GetName().Name}"); }
            return results;
        }
    }
}
