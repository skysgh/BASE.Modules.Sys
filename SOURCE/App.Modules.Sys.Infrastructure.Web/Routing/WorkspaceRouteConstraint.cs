using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System;

namespace App.Modules.Sys.Infrastructure.Web.Routing
{
    /// <summary>
    /// Route constraint to validate workspace IDs.
    /// Ensures first segment isn't a reserved word.
    /// </summary>
    public class WorkspaceRouteConstraint : IRouteConstraint
    {
        public bool Match(
            HttpContext? httpContext,
            IRouter? route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (!values.TryGetValue(routeKey, out var value))
                return false;

            var workspaceId = value?.ToString();
            
            if (string.IsNullOrEmpty(workspaceId))
                return false;

            // Get options from HttpContext if available
            if (httpContext != null)
            {
                var options = httpContext.RequestServices
                    .GetService(typeof(IOptions<WorkspaceRoutingOptions>)) 
                    as IOptions<WorkspaceRoutingOptions>;

                if (options != null && options.Value.IsReserved(workspaceId))
                {
                    return false;  // Reserved word - not a valid workspace
                }
            }

            // Basic validation: alphanumeric, dash, underscore
            foreach (var c in workspaceId)
            {
                if (!char.IsLetterOrDigit(c) && c != '-' && c != '_')
                {
                    return false;
                }
            }

            return true;
        }
    }
}
