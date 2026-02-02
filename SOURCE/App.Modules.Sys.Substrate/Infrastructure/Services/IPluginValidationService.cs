// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

using App.Modules.Sys.Infrastructure.Services.Configuration;

namespace App.Modules.Sys.Infrastructure.Services
{
    /// <summary>
    /// Service to validate a plugin
    /// before loading it up into the same memory.
    /// </summary>
    public interface IPluginValidationService: IHasInfrastructureService
    {
        /// <summary>
        /// Validate the Assembly as being 
        /// maybe ok as a Plugin
        /// </summary>
        /// <param name="assemblyFilePath"></param>
        /// <param name="validationConstraintConfiguration"></param>
        /// <param name="baseSearchDir"></param>
        /// <returns></returns>
        bool ValidateAssembly(
            string assemblyFilePath,
            ValidationConstraintConfiguration? validationConstraintConfiguration = null,
            string? baseSearchDir = null);
    }
}
