using App.Modules.Sys.Infrastructure.Services.Contracts;
using Microsoft.Extensions.Options;

namespace App.Modules.Sys.Infrastructure.Services.Implementations
{
    /// <summary>
    /// Implementation that wraps Microsoft.Extensions.Options.
    /// Isolates external dependency to this assembly only.
    /// </summary>
    /// <typeparam name="T">Configuration type</typeparam>
    public class AppConfiguration<T> : IAppConfiguration<T> where T : class, new()
    {
        private readonly IOptions<T> _options;

        public AppConfiguration(IOptions<T> options)
        {
            _options = options;
        }

        public T Value => _options.Value;

        public T GetValueOrDefault()
        {
            try
            {
                return _options.Value ?? new T();
            }
            catch
            {
                return new T();
            }
        }
    }
}
