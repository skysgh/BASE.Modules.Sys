using App.Modules.Sys.Infrastructure.Diagnostics;
using App.Modules.Sys.Infrastructure.Services;
using App.Modules.Sys.Shared.Models.Enums;
using System;

namespace App.Modules.Sys.Infrastructure.Services.Implementations
{
    /// <summary>
    /// App-specific logger using our own abstractions (no Microsoft ILogger dependency).
    /// Uses IDiagnosticsTracingService for actual tracing with our TraceLevel enum.
    /// </summary>
    public class AppLogger<T> : IAppLogger<T>
    {
        private readonly IDiagnosticsTracingService _tracingService;

        /// <summary>
        /// Constructor - uses our own tracing service (NOT Microsoft ILogger).
        /// </summary>
        public AppLogger(IDiagnosticsTracingService tracingService)
        {
            _tracingService = tracingService ?? throw new ArgumentNullException(nameof(tracingService));
        }

        /// <inheritdoc/>
        public void Log(TraceLevel level, string message) =>
            _tracingService.Trace<T>(level, message);

        /// <inheritdoc/>
        public void LogTrace(string message) => 
            _tracingService.Trace<T>(TraceLevel.Verbose, message);
        
        /// <inheritdoc/>
        public void LogDebug(string message) => 
            _tracingService.Trace<T>(TraceLevel.Debug, message);
        
        /// <inheritdoc/>
        public void LogInformation(string message) => 
            _tracingService.Trace<T>(TraceLevel.Info, message);
        
        /// <inheritdoc/>
        public void LogWarning(string message) => 
            _tracingService.Trace<T>(TraceLevel.Warn, message);
        
        /// <inheritdoc/>
        public void LogError(string message) => 
            _tracingService.Trace<T>(TraceLevel.Error, message);
        
        /// <inheritdoc/>
        public void LogError(Exception exception, string message) => 
            _tracingService.Trace<T>(TraceLevel.Error, $"{message} | Exception: {exception.Message}");
        
        /// <inheritdoc/>
        public void LogCritical(string message) => 
            _tracingService.Trace<T>(TraceLevel.Critical, message);
        
        /// <inheritdoc/>
        public void LogCritical(Exception exception, string message) => 
            _tracingService.Trace<T>(TraceLevel.Critical, $"{message} | Exception: {exception.Message}");
    }
}
