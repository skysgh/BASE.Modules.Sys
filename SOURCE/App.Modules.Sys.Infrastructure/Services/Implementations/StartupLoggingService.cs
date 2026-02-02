//using App.Base.Infrastructure.Configuration;
//using App.Modules.Sys.Infrastructure.Diagnostics;
//using App.Modules.Sys.Infrastructure.Services;
//using App.Modules.Sys.Substrate.Contracts.Models.Enums;
//using App.Modules.Sys.Substrate.Models.Messages;

//// using System;
//// using System.Collections.Generic;
//// using System.Linq;
//// using System.Text;
//// using System.Threading.Tasks;

//namespace App.Modules.Sys.Infrastructure.NewFolder.Services.Implementations
//{
//    /// <summary>
//    /// Implementation of the Startup Logging 
//    /// Service. 
//    /// <para>
//    /// It's so it can persist messages both in the 
//    /// <see cref="StartupLog"/> instance, as well 
//    /// as the logging environment if it is available.
//    /// </para>
//    /// </summary>
//    public class StartupLoggingService : IStartupLoggingService
//    {

//        private IAppLogger? _logger;

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        public StartupLoggingService()
//        {

//        }
//        /// <inheritdoc/>
//        public void SetLogger(IAppLogger appLogger)
//        {
//            _logger = appLogger;
//        }

//        /// <inheritdoc/>
//        public void LogMessage(TraceLevel logLevel, string message)
//        {
//            AppInformation.StartupLog.Journal.Add(message);
//#pragma warning disable CA1848 // Use the LoggerMessage delegates
//#pragma warning disable CA2254 // Template should be a static expression
//            _logger!.Log(logLevel, message);
//#pragma warning restore CA2254 // Template should be a static expression
//#pragma warning restore CA1848 // Use the LoggerMessage delegates
//        }
//    }
//}
