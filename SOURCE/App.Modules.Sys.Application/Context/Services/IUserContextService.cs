using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Application.Context.Services;

/// <summary>
/// Resolved user and workspace context with computed settings.
/// Provides high-level application/business context (who, where, what settings).
/// </summary>
/// <remarks>
/// THIS IS NOT HTTP CONTEXT - This is resolved business context:
/// - User identity (from claims/authentication)
/// - Current workspace (from routing/claims)
/// - Computed settings (cascade resolved)
/// - Tenant context
/// 
/// Lifetime: Scoped (per request, but resolved from multiple sources).
/// 
/// In tests: Can be mocked/stubbed without HTTP context dependency.
/// Example: new Mock&lt;IUserContextService&gt;().Setup(x => x.CurrentUserId).Returns(testUserId);
/// </remarks>
public interface IUserContextService
{
    // ========================================
    // IDENTITY
    // ========================================

    /// <summary>
    /// Current authenticated user's unique identifier.
    /// Resolved from authentication claims (typically "sub" claim).
    /// </summary>
    /// <exception cref="InvalidOperationException">If user not authenticated.</exception>
    Guid CurrentUserId { get; }

    /// <summary>
    /// Current user's username/login name.
    /// Resolved from authentication claims (typically "preferred_username" claim).
    /// </summary>
    string CurrentUserName { get; }

    /// <summary>
    /// Current user's email address.
    /// Resolved from authentication claims (typically "email" claim).
    /// </summary>
    string? CurrentUserEmail { get; }

    /// <summary>
    /// Whether current request has authenticated user.
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// Current user's roles (for authorization).
    /// Resolved from authentication claims (typically "role" claims).
    /// </summary>
    IReadOnlyList<string> Roles { get; }

    /// <summary>
    /// Check if current user has specific role.
    /// </summary>
    bool IsInRole(string role);

    // ========================================
    // WORKSPACE CONTEXT
    // ========================================

    /// <summary>
    /// Current workspace unique identifier.
    /// Resolved from route data or claims (e.g., /{workspaceId}/api/...).
    /// </summary>
    /// <exception cref="InvalidOperationException">If workspace not resolved from route.</exception>
    Guid CurrentWorkspaceId { get; }

    /// <summary>
    /// Current workspace URL-friendly slug.
    /// Resolved from route data (e.g., /acme-corp/api/...).
    /// </summary>
    string CurrentWorkspaceSlug { get; }

    // ========================================
    // TENANT CONTEXT (Multi-Tenant)
    // ========================================

    /// <summary>
    /// Current tenant unique identifier.
    /// Workspaces belong to tenants - this is the parent tenant.
    /// </summary>
    Guid CurrentTenantId { get; }

    // ========================================
    // COMPUTED CONTEXT (Lazy-Loaded)
    // ========================================

    /// <summary>
    /// Get computed settings for current user (cascade resolved: User → Workspace → System).
    /// Cached per request for performance.
    /// </summary>
    /// <remarks>
    /// Expensive operation - resolves entire settings cascade.
    /// Result is cached for duration of request.
    /// </remarks>
    Task<ComputedSettings> GetComputedSettingsAsync(CancellationToken ct = default);

    /// <summary>
    /// Get current user's full profile (display name, avatar, preferences, etc.).
    /// Cached per request for performance.
    /// </summary>
    Task<UserProfile> GetCurrentUserProfileAsync(CancellationToken ct = default);

    /// <summary>
    /// Get current workspace details (name, description, branding, etc.).
    /// Cached per request for performance.
    /// </summary>
    Task<WorkspaceDetails> GetCurrentWorkspaceDetailsAsync(CancellationToken ct = default);
}

/// <summary>
/// Computed settings for current user (cascade resolved).
/// </summary>
public record ComputedSettings
{
    /// <summary>
    /// All effective settings (User → Workspace → System cascade).
    /// </summary>
    public IReadOnlyDictionary<string, string> Settings { get; init; } = new Dictionary<string, string>();

    /// <summary>
    /// Available UI languages for current user.
    /// </summary>
    public IReadOnlyList<SystemLanguageDto> AvailableLanguages { get; init; } = Array.Empty<SystemLanguageDto>();

    /// <summary>
    /// Default language code (ISO 639-1).
    /// </summary>
    public string DefaultLanguageCode { get; init; } = "en";

    /// <summary>
    /// Current user's selected language code.
    /// </summary>
    public string CurrentLanguageCode { get; init; } = "en";

    /// <summary>
    /// Current workspace branding configuration.
    /// </summary>
    public WorkspaceBranding? Branding { get; init; }
}

/// <summary>
/// User profile information.
/// </summary>
public record UserProfile
{
    /// <summary>User unique identifier.</summary>
    public Guid UserId { get; init; }
    
    /// <summary>User login name.</summary>
    public string UserName { get; init; } = null!;
    
    /// <summary>User display name.</summary>
    public string? DisplayName { get; init; }
    
    /// <summary>User email address.</summary>
    public string? Email { get; init; }
    
    /// <summary>User avatar URL.</summary>
    public string? AvatarUrl { get; init; }
    
    /// <summary>User timezone.</summary>
    public string? TimeZone { get; init; }
}

/// <summary>
/// Workspace details.
/// </summary>
public record WorkspaceDetails
{
    /// <summary>Workspace unique identifier.</summary>
    public Guid WorkspaceId { get; init; }
    
    /// <summary>Workspace URL slug.</summary>
    public string Slug { get; init; } = null!;
    
    /// <summary>Workspace name.</summary>
    public string Name { get; init; } = null!;
    
    /// <summary>Workspace description.</summary>
    public string? Description { get; init; }
    
    /// <summary>Workspace branding.</summary>
    public WorkspaceBranding? Branding { get; init; }
}

/// <summary>
/// Workspace branding configuration.
/// </summary>
public record WorkspaceBranding
{
    /// <summary>Logo URL.</summary>
    public string? LogoUrl { get; init; }
    
    /// <summary>Primary color.</summary>
    public string? PrimaryColor { get; init; }
    
    /// <summary>Secondary color.</summary>
    public string? SecondaryColor { get; init; }
}

/// <summary>
/// DTO reference (to avoid circular dependency - actual definition in Application layer).
/// </summary>
public record SystemLanguageDto
{
    /// <summary>Language ID.</summary>
    public Guid Id { get; init; }
    
    /// <summary>Language code.</summary>
    public string Code { get; init; } = null!;
    
    /// <summary>Language name.</summary>
    public string Name { get; init; } = null!;
    
    /// <summary>Native language name.</summary>
    public string? NativeName { get; init; }
    
    /// <summary>Language icon URL.</summary>
    public string? IconUrl { get; init; }
    
    /// <summary>Whether language is active.</summary>
    public bool IsActive { get; init; }
    
    /// <summary>Whether language is default.</summary>
    public bool IsDefault { get; init; }
    
    /// <summary>Sort order.</summary>
    public int SortOrder { get; init; }
}
