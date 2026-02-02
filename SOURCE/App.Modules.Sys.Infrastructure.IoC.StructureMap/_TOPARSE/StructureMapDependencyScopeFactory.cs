
namespace App.Host.ECSD.DependencyResolution
{
    public class StructureMapDependencyScopeFactory
    {
        public static StructureMapDependencyScope ConfigureContainer()
        {
            // Use the Singleton to make a container
            // that has its Scanining defined:
            var container = IoC.Initialize();
            var result = new StructureMapDependencyScope(container);
            return result;
        }
    }
}