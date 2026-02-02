using App.Modules.Sys.Initialisation;
using App.Modules.Sys.Initialisation.Implementation.Base;
using App.Modules.Sys.Initialisation.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Modules.Sys.Domain.Initialisation
{
    /// <summary>
    /// Assembly specific implementation of
    /// <see cref="IModuleAssemblyInitialiser"/>
    /// </summary>
    public class ModuleAssemblyInitialiser : IModuleAssemblyInitialiser
    {
        /// <inheritdoc/>
        public void DoAfterBuild()
        {
        }

        ///<inheritdoc/>
        public void DoBeforeBuild()
        {
        }
    }
}
