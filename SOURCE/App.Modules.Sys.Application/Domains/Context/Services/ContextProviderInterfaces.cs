using App.Modules.Sys.Application.Domains.Context.Models.Implementations;

namespace App.Modules.Sys.Application.Domains.Context.Services;

/// <summary>
/// Service to provide system-level context data.
/// </summary>
public interface ISystemContextProvider
{
    /// <summary>
    /// Get system context (branding, version, features).
    /// </summary>
    Task<SystemContextDto> GetSystemContextAsync(CancellationToken ct = default);
}

/// <summary>
/// Service to provide workspace-level context data.
/// </summary>
public interface IWorkspaceContextProvider
{
    /// <summary>
    /// Get workspace context for current user.
    /// </summary>
    Task<WorkspaceContextDto?> GetWorkspaceContextAsync(CancellationToken ct = default);
}

/// <summary>
/// Service to provide navigation context.
/// </summary>
public interface INavigationContextProvider
{
    /// <summary>
    /// Get navigation context (menus, breadcrumbs).
    /// </summary>
    Task<NavigationContextDto> GetNavigationContextAsync(string currentRoute, CancellationToken ct = default);
}

/// <summary>
/// Service to provide computed settings.
/// </summary>
public interface ISettingsContextProvider
{
    /// <summary>
    /// Get effective settings for current user/workspace.
    /// </summary>
    Task<ComputedSettingsDto> GetSettingsContextAsync(CancellationToken ct = default);
}
