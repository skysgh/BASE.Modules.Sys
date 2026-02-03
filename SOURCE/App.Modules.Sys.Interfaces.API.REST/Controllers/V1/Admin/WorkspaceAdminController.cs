using App.Modules.Sys.Application.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Interfaces.API.REST.Controllers.V1.Admin
{
    /// <summary>
    /// Admin API for workspace management.
    /// Allows cache refresh when workspaces are added/removed.
    /// </summary>
    [ApiController]
    [Route("api/admin/workspaces")]
    public class WorkspaceAdminController : ControllerBase
    {
        private readonly IWorkspaceValidationService _workspaceValidator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="workspaceValidator"></param>
        public WorkspaceAdminController(IWorkspaceValidationService workspaceValidator)
        {
            _workspaceValidator = workspaceValidator;
        }

        /// <summary>
        /// Refresh workspace cache.
        /// Call this after adding/removing workspaces to update the in-memory cache.
        /// </summary>
        /// <remarks>
        /// Example:
        /// POST /api/admin/workspaces/refresh-cache
        /// 
        /// Returns: 200 OK
        /// </remarks>
        [HttpPost("refresh-cache")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RefreshCache(CancellationToken ct = default)
        {
            if (_workspaceValidator is IWorkspaceCacheManager cacheManager)
            {
                await cacheManager.RefreshCacheAsync(ct);
                return Ok(new { message = "Workspace cache refreshed successfully" });
            }

            return BadRequest(new { message = "Cache refresh not supported by current implementation" });
        }
    }
}

