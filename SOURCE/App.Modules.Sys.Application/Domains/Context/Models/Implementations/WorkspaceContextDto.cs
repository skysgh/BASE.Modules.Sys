namespace App.Modules.Sys.Application.Domains.Context.Models.Implementations;

/// <summary>
/// Workspace/tenant-level context information.
/// Supports multi-workspace membership.
/// </summary>
public record WorkspaceContextDto
{
    /// <summary>
    /// All workspaces the user is a member of.
    /// </summary>
    public List<WorkspaceSummaryDto> MemberOf { get; init; } = new();

    /// <summary>
    /// Current/active workspace (the one user is operating in).
    /// </summary>
    public WorkspaceDetailsDto? Current { get; init; }
}

/// <summary>
/// Summary information for a workspace (used in memberOf list).
/// </summary>
public record WorkspaceSummaryDto
{
    /// <summary>
    /// Workspace unique identifier.
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Workspace display name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// User's role in this workspace.
    /// </summary>
    public string Role { get; init; } = string.Empty;

    /// <summary>
    /// Whether this is the default workspace for the user.
    /// </summary>
    public bool IsDefault { get; init; }
}

/// <summary>
/// Detailed information for the current/active workspace.
/// </summary>
public record WorkspaceDetailsDto
{
    /// <summary>
    /// Workspace unique identifier.
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Workspace display name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Workspace-level branding (tenant customization).
    /// </summary>
    public WorkspaceBrandingDto Branding { get; init; } = new();

    /// <summary>
    /// Account/subscription information.
    /// </summary>
    public AccountInfoDto Account { get; init; } = new();

    /// <summary>
    /// Workspace-level settings.
    /// </summary>
    public Dictionary<string, object> Settings { get; init; } = new();
}

/// <summary>
/// Workspace branding customization.
/// </summary>
public record WorkspaceBrandingDto
{
    /// <summary>
    /// Organization name.
    /// </summary>
    public string OrganizationName { get; init; } = string.Empty;

    /// <summary>
    /// Logo URL.
    /// </summary>
    public string? LogoUrl { get; init; }

    /// <summary>
    /// Primary brand color (hex).
    /// </summary>
    public string? PrimaryColor { get; init; }

    /// <summary>
    /// Custom CSS URL.
    /// </summary>
    public string? CustomCssUrl { get; init; }
}

/// <summary>
/// Account/subscription tier information.
/// </summary>
public record AccountInfoDto
{
    /// <summary>
    /// Subscription tier (Free, Pro, Enterprise).
    /// </summary>
    public string Tier { get; init; } = "Free";

    /// <summary>
    /// Maximum user limit for this account.
    /// </summary>
    public int? MaxUsers { get; init; }

    /// <summary>
    /// Storage limit in GB.
    /// </summary>
    public int? StorageLimitGb { get; init; }

    /// <summary>
    /// Whether this account is in good standing.
    /// </summary>
    public bool IsActive { get; init; } = true;
}
