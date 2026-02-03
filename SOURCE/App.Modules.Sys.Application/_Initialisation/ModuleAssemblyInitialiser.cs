using App.Modules.Sys.Domain.Initialisation;
using App.Modules.Sys.Initialisation;
using App.Modules.Sys.Initialisation.Implementation.Base;
using App.Modules.Sys.Substrate.Models.Messages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Modules.Sys.Application.Initialisation
{
    /// <summary>
    /// Assembly specific implementation of
    /// <see cref="IModuleAssemblyInitialiser"/>
    /// </summary>
    public class ModuleAssemblyInitialiser : ModuleAssemblyInitialiserBase
    {
        ///<inheritdoc/>
        public override void DoBeforeBuild(IServiceCollection services)
        {
        }

        /// <inheritdoc/>
        public override void DoAfterBuild(IServiceProvider serviceProvider)
        {
        }
    }

}
