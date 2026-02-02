// using App.Modules.Sys.Presentation.Web.APIs.Constants;
// using App.Modules.Sys.Infrastructure.Services;
// using App.Modules.Sys.Shared.Models.Entities;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

//namespace App.Modules.Sys.Application.Services.Implementations.Base
//{


//    /// <summary>
//    /// All Controllers, whatever module,  *should* inherit in one way or another
//    /// from this base controller.
//    /// <para>
//    /// The advantages include:
//    /// * only one class that needs to be updated to .NET Core when we get there.
//    /// * ensures that all classes are injected with an implementation of 
//    /// <see cref="IDiagnosticsTracingService"/> and <see cref="IPrincipalService"/>
//    /// so there is absolutely no excuse for poor diagnostics logs, or security...
//    /// (that said, still don't trust developers rushing to meet deadlines to take 
//    /// care of ISO-25010/Maintainability or ISO-25010/Security, so we handle 
//    /// Security and Logging as GLobal Filters anyway).
//    /// </para>
//    /// </summary>
//    /// <seealso cref="System.Web.OData.ODataController" />
//    [Route(Route)]
//    public abstract class CommonODataControllerBase : ODataController
//    {
//      /// <summary>
//      /// Name of Controller.
//      /// <para>
//      /// Unique per Protocol per Module.
//      /// </para>
//      /// <para>
//      /// Combined with the Module Key
//      /// used to develop the 
//      /// Route to the Controller.
//      /// </para>
//      /// <para>
//      /// Note that when a Controller is OData enabled, 
//      /// the Name is used to register the 
//      /// route in the OData EDM Model.
//      /// </para>
//      /// </summary>
//       internal const string Name...
//
//       /// <summary>
//       /// The unique Route to the Controller, combining
//       /// <list type="bullet">
//       /// <item>Module Key</item>
//       /// <item>Protocol based Service Root</item>
//       /// <item>API Version</item>
//       /// <item>Controller Name</item>
//       /// </list>
//       /// </summary>
//internal const string Route = $"{ModuleApiConstants.ApiODataRoutePrefix}/{Name}";



//        protected readonly IDiagnosticsTracingService _diagnosticsTracingService;
//        protected readonly IPrincipalService _principalService;

//        protected CommonODataControllerBase(
//            IDiagnosticsTracingService diagnosticsTracingService, 
//            IPrincipalService principalService)
//        {
//            this._diagnosticsTracingService = diagnosticsTracingService;
//            this._principalService = principalService;

//            this._diagnosticsTracingService.Trace(
//                TraceLevel.Debug, $"{this.GetType().Name} created.");

//        }
//    }
//}