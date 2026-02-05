using App.Modules.Sys.Shared.Services.Registration;

namespace App.Modules.Sys.Infrastructure.Domains.Configuration
{
    /// <inheritdoc/>
    public class ServiceDefinition : IServiceDefinition
    {
        /// <inheritdoc/>
        public Type ContractType { get; set; }= default!;
        /// <inheritdoc/>
        public Type ImplementationType { get; set; }=   default!;
    }

    /// <inheritdoc/>
    public class ServiceDefinition<TContract, TInstance> : IServiceDefinition<TContract, TInstance>
    {
        /// <inheritdoc/>
        public Type ContractType { get; set; } = typeof(TContract);
        /// <inheritdoc/>
        public Type ImplementationType { get; set; } = typeof(TInstance);
    }
}
