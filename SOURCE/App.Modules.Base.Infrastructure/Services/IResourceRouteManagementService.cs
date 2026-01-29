// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

namespace App.Modules.Base.Infrastructure.NewFolder.Services
{
    /// <summary>
    /// Contract for handling end user Routes 
    /// and rewriting them as necessary.
    /// </summary>
    public interface IResourceRouteManagementService : IHasAppBaseInfrastructureService
    {
        /// <summary>
        /// Rewrites the provided request if/as required.
        /// </summary>
        /// <param name="resourceRoute"></param>
        /// <returns></returns>
        public string? Rewrite(string? resourceRoute);
    }
}
