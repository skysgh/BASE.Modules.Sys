// using System;
// using System.Collections.Generic;
// using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
// using System.Text;
// using System.Threading.Tasks;

namespace App.Base.Infrastructure.ModuleManagement
{

    /// <summary>
    /// App Module Custom 
    /// <see cref="AssemblyLoadContext"/>
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    /// <param name="pluginPath"></param>
    public class AppModuleLoadContext(string pluginPath) : AssemblyLoadContext
    {
        private readonly AssemblyDependencyResolver _resolver = new(pluginPath);

        /// <inheritdoc/>

        protected override Assembly? Load(AssemblyName assemblyName)
        {
            string? assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
#pragma warning disable IDE0046 // Convert to conditional expression
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }
#pragma warning restore IDE0046 // Convert to conditional expression
            return null;
        }
        /// <inheritdoc/>

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string? libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
#pragma warning disable IDE0046 // Convert to conditional expression
            if (libraryPath != null)
            {
                return LoadUnmanagedDllFromPath(libraryPath);
            }
#pragma warning restore IDE0046 // Convert to conditional expression
            return IntPtr.Zero;
        }
    }
}
