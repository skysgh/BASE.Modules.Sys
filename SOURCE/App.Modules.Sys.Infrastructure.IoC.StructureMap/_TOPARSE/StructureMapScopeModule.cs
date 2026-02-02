using System.Web;
using StructureMap.Web.Pipeline;

namespace App.Host.ECSD.DependencyResolution
{

    public class StructureMapScopeModule : IHttpModule
    {
        #region Public Methods and Operators

        public void Dispose()
        {
        }

        /// <summary>
        /// Initializes the.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, e) => StructuremapStartup.StructureMapDependencyScope.CreateNestedContainer();
            context.EndRequest += (sender, e) => {
                HttpContextLifecycle.DisposeAndClearAll();
                StructuremapStartup.StructureMapDependencyScope.DisposeNestedContainer();
            };
        }

        #endregion
    }
}