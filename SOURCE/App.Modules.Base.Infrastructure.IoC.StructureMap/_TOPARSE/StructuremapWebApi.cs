using App.Host.ECSD.DependencyResolution;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(StructuremapWebApi), "Start")]
namespace App.Host.ECSD.DependencyResolution
{
    public static class StructuremapWebApi
    {
        public static void Start()
        {
            //var structureMapDependencyScope = StructureMapDependencyScopeFactory.ConfigureContainer();
            //// --------------------------------------------------
            ////Register container so that App.Core.Infrastructure.ServiceLocatorInitiator
            ////Can get back to it:
            //StructureMapContainerLocator.Register(
            //    () => structureMapDependencyScope.Container);
            var container = StructuremapStartup.StructureMapDependencyScope.Container;
            //GlobalConfiguration.Configuration.DependencyResolver = new StructureMapWebApiDependencyResolver(container);
            // Have to use our new instance of HttpConfiguration
            HttpConfigurationLocator.Current.DependencyResolver = new StructureMapWebApiDependencyResolver(container);
            // Do not call yet:
            //GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}