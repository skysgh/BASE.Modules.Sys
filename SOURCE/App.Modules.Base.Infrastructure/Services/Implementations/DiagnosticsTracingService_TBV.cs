namespace App.Base.Infrastructure.Services.Implementations
{
    using System.Collections.Generic;
    using System.Threading;
    using App.Base.Shared.Models.Entities;
    /// <summary>
    ///     Implementation of the
    ///     <see cref="IDiagnosticsTracingService" />
    ///     Infrastructure Service Contract
    /// </summary>
    /// <seealso cref="App.Base.Infrastructure.Services.IDiagnosticsTracingService" />
    public class DiagnosticsTracingService : AppCoreServiceBase, IDiagnosticsTracingService
    {
        private static readonly Queue<TraceEntry> _cache = new Queue<TraceEntry>();
        private static TraceLevel _flushLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticsTracingService"/> class.
        /// </summary>
        public DiagnosticsTracingService()
        {
            // Needs to be wired up to Application Settings to be dynamic in order to not need a restart
            // when errors start happening.
            _flushLevel = TraceLevel.Verbose;
        }

        /// <inheritdoc/>
        public void Trace(TraceLevel traceLevel, string message, params object[] arguments)
        {
            _cache.Enqueue(new TraceEntry {TracelLevel = traceLevel, Message = message, Args = arguments});
            if (_cache.Count > 100)
            {
                lock (this)
                {
                    while (_cache.Count > 100)
                    {
                        _cache.Dequeue();
                    }
                }
            }
            if (traceLevel <= _flushLevel)
            {
                lock (this)
                {
                    while (_cache.Count > 0)
                    {
                        var x = _cache.Dequeue();
                        if (x != null)
                        {
                            DirectTrace(x.TracelLevel, x.Message, x.Args);
                        }
                    }
                }
            }
        }


        private void DirectTrace(TraceLevel traceLevel, string message, params object[] arguments)
        {
            const string lineEnding = "\r\n";

            if (arguments != null && arguments.Length > 0)
            {
                message = string.Format(message, arguments);
            }
           

            var threadId = Thread.CurrentThread.Name?? Thread.CurrentThread.ManagedThreadId.ToString();

            switch (traceLevel)
            {
                // The trouble with TraceError/TraceWarning is that they don't just add one line, 
                // they add multiple lines,
                // a Header, then the Line, then Footer.
                // The header has a format of "{source} {type}: {id} :"
                // The footer adds one or more indented lines:
                //   "ProcessId={processId}"
                //   "LogicalOperationStack={....}
                //   ""ThreadId=" + eventCache.ThreadId);
                //   "DateTime="
                //   "Timestamp="
                //   "Callstack="
                // In other words, it will be slower than just writing one line using WriteLine.

                // That said, WriteLine will be prefixed with "Verbose"
                // So you can use plain old Write, terminated with "\r\n"
                case TraceLevel.Critical:
                     //System.Diagnostics.Trace.TraceError(message);
                    //System.Diagnostics.Trace.WriteLine($"CRITICAL: {message}");
                    System.Diagnostics.Trace.Write($"CRITICAL: {threadId}: {message}{lineEnding}");
                    break;
                case TraceLevel.Error:
                    // System.Diagnostics.Trace.TraceError(message);
                    //System.Diagnostics.Trace.WriteLine($"ERROR...: {message}");
                    System.Diagnostics.Trace.Write($"ERROR...: {threadId}: {message}{lineEnding}");
                    break;
                case TraceLevel.Warn:
                    //System.Diagnostics.Trace.TraceWarning(message);
                    //System.Diagnostics.Trace.WriteLine($"WARN....: {message}");
                    System.Diagnostics.Trace.Write($"WARN....: {threadId}: {message}{lineEnding}");
                    break;
                case TraceLevel.Info:
                    //System.Diagnostics.Trace.TraceInformation(message);
                    // System.Diagnostics.Trace.WriteLine($"INFO....: {message}");
                    System.Diagnostics.Trace.Write($"INFO....: {threadId}: {message}{lineEnding}");
                    break;
                case TraceLevel.Debug:
                    // System.Diagnostics.Trace.WriteLine($"DEBUG...: {message}");
                    System.Diagnostics.Trace.Write($"DEBUG: {threadId}: {message}{lineEnding}");
                    break;
                case TraceLevel.Verbose:
                    // System.Diagnostics.Trace.WriteLine($"DEBUG...: {message}");
                    System.Diagnostics.Trace.Write($"VERBOSE: {threadId}: {message}{lineEnding}");
                    break;
            }

        }

        private class TraceEntry
        {
            public object[] Args;
            public string Message;
            public TraceLevel TracelLevel;
        }
    }
}