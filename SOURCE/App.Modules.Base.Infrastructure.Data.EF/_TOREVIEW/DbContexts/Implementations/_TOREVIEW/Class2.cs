using App.Modules.Base.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Modules.Base.Infrastructure.Storage.EF
{
    public static class FuckFuckFuck
    {

        public static void Crap(IServiceCollection services, ConfigurationManager configuration)
        {
            string check = configuration.GetConnectionString("default");

            // Add DbContext:
            // Except that means a dependency on EF
            // From Host...not the best. Better if it could be added later.
            // But how... Since Lamar ServiceRepository doesn't have view
            // back to builder...
            services.AddDbContext<BaseModuleDbContext>(
                options =>
            options.UseSqlServer(
                configuration.GetConnectionString("default")
                )
            );

        }

    }
}