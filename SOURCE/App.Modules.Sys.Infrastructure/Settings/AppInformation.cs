
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

using App.Modules.Sys.Shared.Models.Implementations;

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
        public static AppConfigurationObject Configuration { get; } = new();


        /// <summary>
        /// Log of initialisation steps.
        /// </summary>
        public static StartupLog StartupLog { get; } = new();

    }
}