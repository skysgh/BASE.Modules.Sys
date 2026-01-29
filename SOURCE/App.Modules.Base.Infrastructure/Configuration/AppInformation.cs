using App.Modules.Base.Substrate.Models.Messages;

// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

namespace App.Base.Infrastructure.Configuration
{
    /// <summary>
    /// Information about the application.
    /// </summary>
    public static class AppInformation
    {
        /// <summary>
        /// Context information of Application
        /// </summary>
        public static AppContext Context { get; } = new();

        /// <summary>
        /// Configuration of Application
        /// </summary>
        public static AppConfiguration Configuration { get; } = new();


        /// <summary>
        /// Log of initialisation steps.
        /// </summary>
        public static AppInitialisationLog StartupLog { get; } = new();

    }
}