using App.Modules.Sys.Initialisation.Implementation.Base;
using Microsoft.Extensions.DependencyInjection;

namespace App.Modules.Sys.Infrastructure.AutoMapper.Initialisation
{
    /// <summary>
    /// AutoMapper infrastructure module initializer.
    /// Registers AutoMapper as the IObjectMappingService implementation.
    /// </summary>
    /// <remarks>
    /// Plugin architecture:
    /// - This module is OPTIONAL
    /// - Can be replaced with alternative mapping library
    /// - No other code knows about AutoMapper
    /// </remarks>
    public class AutoMapperModuleAssemblyInitialiser : ModuleAssemblyInitialiserBase
    {
        /// <inheritdoc/>
        public override void DoBeforeBuild(IServiceCollection services)
        {
            // Register AutoMapper-based object mapping
            // Scans Application assemblies for ObjectMapBase<,> implementations
            services.AddAutoMapperObjectMapping();
        }

        /// <inheritdoc/>
        public override void DoAfterBuild(IServiceProvider serviceProvider)
        {
            // Nothing needed post-build for AutoMapper
        }
    }
}
