using App.Modules.Sys.Initialisation.Implementation.Base;

namespace App.Modules.Sys.Infrastructure.Services.Azure.Initialisation
{
    /// <summary>
    /// Optional custom initializer for Azure infrastructure services.
    /// Override DoBeforeBuild() or DoAfterBuild() if needed.
    /// </summary>
    public class InfrastructureAzureModuleAssemblyInitialiser : ModuleAssemblyInitialiserBase
    {
        /// <inheritdoc/>
        public override void DoBeforeBuild()
        {
            // Custom logic before DI builds
        }
        
        /// <inheritdoc/>
        public override void DoAfterBuild()
        {
            // Custom logic after DI builds (has IServiceProvider)
        }
    }
}



