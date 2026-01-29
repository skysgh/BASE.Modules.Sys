using App.Modules.Core.Application.APIs.Messages.V0100.Demo;
using App.Modules.Core.Application.Services.Implementations.Base;
using App.Modules.Core.Shared.Models.Entities;
using App.Modules.Core.Infrastructure.NewFolder.Services;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Modules.Core.Application.Interfaces.APIs.Services.Implementations._TOREVIEW
{

    /// <summary>
    /// Application Service
    /// to view Diagnostics Trace Entries.
    /// </summary>
    public class DiagnosticsManagementService :
        DtoServiceBase
            <DiagnosticsTraceEntry,
            DiagnosticsLogEntryDto>,
        IDiagnosticsManagementService
    {
        private readonly IDiagnosticsManagementService _diagnosticsManagementService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="diagnosticsManagementService"></param>
        /// <param name="contextIdentifier"></param>
        /// <param name="diagnosticsTracingService"></param>
        /// <param name="principalService"></param>
        /// <param name="queryableService"></param>
        /// <param name="objectMappingService"></param>
        public DiagnosticsManagementService(
            IDiagnosticsManagementService diagnosticsManagementService,
            string contextIdentifier,
            IDiagnosticsTracingService diagnosticsTracingService,
            IPrincipalService principalService,
            IQueryableService<DiagnosticsTraceEntry> queryableService,
            IObjectMappingService objectMappingService)
            : base(
                  contextIdentifier,
                  diagnosticsTracingService,
                  principalService,
                  queryableService,
                  objectMappingService)
        {
            _diagnosticsManagementService = diagnosticsManagementService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<DiagnosticsLogEntryDto> Get()
        {
            return _diagnosticsManagementService.Get();
        }
    }
}
