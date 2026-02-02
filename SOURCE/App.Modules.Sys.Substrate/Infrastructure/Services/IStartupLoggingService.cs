//// using System;
//// using System.Collections.Generic;
//// using System.Linq;
//// using System.Text;
//// using System.Threading.Tasks;

//using App.Modules.Sys.Substrate.Contracts.Models.Enums;

//namespace App.Modules.Sys.Infrastructure.Services
//{
//    /// <summary>
//    /// Contract to persist information
//    /// as to startup.
//    /// </summary>
//    public interface IStartupLoggingService : IHasAppInfrastructureService
//    {

//        /// <summary>
//        /// Invoke during startup.
//        /// </summary>
//        /// <param name="appLogger"></param>
//        public void SetLogger(ILogger appLogger);

//        /// <summary>
//        /// Journal a single message:
//        /// </summary>
//        /// <param name="logLevel"></param>
//        /// <param name="message"></param>
//        public void LogMessage(TraceLevel logLevel, string message);
//    }
//}
