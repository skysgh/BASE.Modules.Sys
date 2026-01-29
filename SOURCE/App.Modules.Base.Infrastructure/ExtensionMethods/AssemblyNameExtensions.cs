// using System;
// using System.Collections.Generic;
// using System.Linq;
using System.Reflection;
// using System.Text;
// using System.Threading.Tasks;

namespace App
{

    /// <summary>
    /// Extension methods to the Assembly type.
    /// </summary>
    public static class AssemblyNameExtensions
    {
        /// <summary>
        /// Gets the Module Name from the Assembly
        /// (presumably a Presentation Assembly 
        /// containing the Controller Action found
        /// via the Route)
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="result"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static bool GetAppModuleName(
            this AssemblyName assemblyName, out string? result, out string? prefix)
        {
            string? name = assemblyName.Name;
            if (name == null)
            {
                result = null;
                prefix = null;
                return false;
            }
#pragma warning disable CA1310 // Specify StringComparison for correctness
            if (!name.StartsWith("App."))
            {
                //It's not following convention.
                result = null;
                prefix = null;
                return false;
            }
#pragma warning restore CA1310 // Specify StringComparison for correctness
            string[] parts = name.Split('.');
            if (parts[1] == "Modules")
            {
                // It's going to be 'Foo' of 'App.Modules.Foo'
                result = parts[2];
                prefix = string.Join('.', parts.Take(3)) + '.';
                return true;
            }
            // It's going to be 'Base' of App.Base. or 'Host'
            result = parts[1];
            prefix = string.Join('.', parts.Take(2)) + '.';
            return true;
        }
    }
}
