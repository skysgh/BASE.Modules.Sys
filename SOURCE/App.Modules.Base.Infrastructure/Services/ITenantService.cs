using App.Modules.Base.Substrate.Models.CacheItems;

// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

namespace App.Modules.Base.Infrastructure.NewFolder.Services
{
    /// <summary>
    /// Contract for a service to return Cacheable
    /// info regarding a tenant.
    /// </summary>
    public interface ITenantInfoService : IHasAppBaseInfrastructureService
    {
        /// <summary>
        /// Find infrastructure required 
        /// info as to a Tenant.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="returnDefaultIfNotFound"></param>
        /// <returns></returns>
        TenancyInfo Find(string tenantId, bool returnDefaultIfNotFound = true);
    }
}
