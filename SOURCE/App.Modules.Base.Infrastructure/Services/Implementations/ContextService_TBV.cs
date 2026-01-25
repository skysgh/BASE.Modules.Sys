namespace App.Base.Infrastructure.Services.Implementations
{
    using System.Web;

    /// <summary>
    ///     Implementation of the
    ///     <see cref="IContextService" />
    ///     Infrastructure Service Contract
    /// </summary>
    /// <seealso cref="App.Base.Infrastructure.Services.IContextService" />
    public class ContextService : AppCoreServiceBase, IContextService
    {
        public void Set(string key, object value)
        {
            HttpContext.Current.Items[key] = value;
        }

        public object Get(string key)
        {
            return HttpContext.Current.Items[key];
        }
    }
}