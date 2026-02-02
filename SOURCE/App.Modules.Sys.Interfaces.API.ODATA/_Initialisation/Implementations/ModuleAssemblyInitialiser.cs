using App.Modules.Sys.Initialisation;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Modules.Sys.Interfaces.API.ODATA.Initialisation.Implementations
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
