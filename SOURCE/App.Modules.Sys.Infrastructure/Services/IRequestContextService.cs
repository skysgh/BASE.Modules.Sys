using System;
using System.Collections.Generic;

namespace App.Modules.Sys.Infrastructure.Services;

/// <summary>
/// HTTP request-scoped context (extends base execution context).
/// Provides access to raw HTTP request data (headers, IP, route, user agent, etc.).
/// </summary>
/// <remarks>
/// Lifetime: Scoped (per HTTP request).
/// 
/// Inheritance: IRequestContextService : IExecutionContextService
/// - Base properties (ExecutionId, ExecutionType, etc.) from IExecutionContextService
/// - HTTP-specific properties (HttpMethod, Headers, IpAddress, etc.) from this interface
/// 
/// THIS IS INFRASTRUCTURE CONCERN - Raw HTTP data only.
/// For business context (user, workspace, settings), use IUserContextService instead.
/// 
/// NOTE: This interface does NOT directly reference Microsoft.AspNetCore.Http.HttpContext
/// to avoid forcing ASP.NET Core dependency on base Infrastructure assembly.
/// Implementations (in Infrastructure.Web) will provide HttpContext access.
/// 
/// Use cases:
/// - Logging (request ID, IP, user agent)
/// - Diagnostics (headers, timing)
/// - Security (IP filtering, rate limiting)
/// - Device detection (parse user agent)
/// </remarks>
public interface IRequestContextService : IExecutionContextService
{
    // ========================================
    // HTTP-SPECIFIC PROPERTIES
    // (Base properties inherited from IExecutionContextService)
    // ========================================

    // ========================================
    // REQUEST IDENTIFICATION
    // ========================================

    /// <summary>
    /// Request path (e.g., "/api/v1/users").
    /// </summary>
    string RequestPath { get; }

    /// <summary>
    /// HTTP method (GET, POST, PUT, DELETE, etc.).
    /// </summary>
    string HttpMethod { get; }

    /// <summary>
    /// Query string (e.g., "?page=1&amp;size=20").
    /// </summary>
    string? QueryString { get; }

    // ========================================
    // CLIENT INFORMATION
    // ========================================

    /// <summary>
    /// Client IP address (IPv4 or IPv6).
    /// Considers X-Forwarded-For header if behind proxy/load balancer.
    /// </summary>
    string IpAddress { get; }

    /// <summary>
    /// User-Agent header value.
    /// Contains browser/client information.
    /// </summary>
    string? UserAgent { get; }

    /// <summary>
    /// Referer header value (page that linked to this request).
    /// </summary>
    string? Referer { get; }

    // ========================================
    // HEADERS
    // ========================================

    /// <summary>
    /// All HTTP request headers.
    /// </summary>
    IReadOnlyDictionary<string, string> Headers { get; }

    /// <summary>
    /// Get specific header value by name.
    /// </summary>
    /// <param name="headerName">Header name (case-insensitive).</param>
    /// <returns>Header value if exists, null otherwise.</returns>
    string? GetHeader(string headerName);

    // ========================================
    // SECURITY
    // ========================================

    /// <summary>
    /// Whether request is over HTTPS.
    /// </summary>
    bool IsSecureConnection { get; }

    /// <summary>
    /// Whether request is from localhost.
    /// Useful for development/diagnostics features.
    /// </summary>
    bool IsLocalRequest { get; }

    // ========================================
    // ROUTE DATA
    // ========================================

    /// <summary>
    /// Route values extracted from URL pattern.
    /// Example: For route /{workspaceId}/api/users/{userId}
    /// RouteData["workspaceId"] = "123", RouteData["userId"] = "456"
    /// </summary>
    IReadOnlyDictionary<string, object?> RouteData { get; }

    /// <summary>
    /// Get specific route value by key.
    /// </summary>
    /// <param name="key">Route parameter name.</param>
    /// <returns>Route value if exists, null otherwise.</returns>
    object? GetRouteValue(string key);
}
