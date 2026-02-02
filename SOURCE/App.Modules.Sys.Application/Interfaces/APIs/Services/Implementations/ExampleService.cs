// using App.Modules.Sys.Application.Interfaces.APIs.Services;

namespace App.Modules.Sys.Application.Interfaces.APIs.Services.Implementations
{
    /// <inheritdoc/>
    public class ExampleService : IExampleService
    {
        /// <inheritdoc/>
        public bool DoSomething()
        {
            return true;
        }

        /// <inheritdoc/>
        public string Hello()
        {
            // TODO: change to use Resource string.
            return "Hello";
        }
    }
}
