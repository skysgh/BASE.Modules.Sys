using System.Web.Http.Dependencies;
using StructureMap;

namespace App.Host.ECSD.DependencyResolution
{
    public class StructureMapWebApiDependencyScope : StructureMapDependencyScope, IDependencyScope
    {
        public StructureMapWebApiDependencyScope(IContainer container)
            : base(container)
        {
        }
    }
}