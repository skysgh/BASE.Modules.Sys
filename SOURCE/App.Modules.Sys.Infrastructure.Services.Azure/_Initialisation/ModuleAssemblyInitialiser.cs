using App.Modules.Sys.Initialisation.Implementation.Base;
using Microsoft.Extensions.DependencyInjection;

namespace App.Modules.Sys.Infrastructure.Services.Azure.Initialisation
{
    /// <summary>
    /// Optional custom initializer for Azure infrastructure services.
    /// Override DoBeforeBuild() or DoAfterBuild() if needed.
    /// </summary>
    public class InfrastructureAzureModuleAssemblyInitialiser : ModuleAssemblyInitialiserBase
    {
        /// <inheritdoc/>
        public override void DoBeforeBuild(IServiceCollection services)
        {
            // Custom logic before DI builds
        }
        
        /// <inheritdoc/>
        public override void DoAfterBuild(IServiceProvider serviceProvider)
        {
            // Custom logic after DI builds (has IServiceProvider)
        }
    }
}



