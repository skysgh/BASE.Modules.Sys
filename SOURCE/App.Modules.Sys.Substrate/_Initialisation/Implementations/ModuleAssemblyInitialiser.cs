using App.Modules.Sys.Initialisation.Implementation;
using App.Modules.Sys.Initialisation.Implementation.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Modules.Sys.Initialisation.Implementations
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
