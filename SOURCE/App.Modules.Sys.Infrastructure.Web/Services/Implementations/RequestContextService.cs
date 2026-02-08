using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using App.Modules.Sys.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace App.Modules.Sys.Infrastructure.Web.Services.Implementations;

/// <summary>
/// HTTP request context implementation - SINGLETON.
/// Provides per-request storage, service resolution, and HTTP request data.
/// </summary>
/// <remarks>
/// Lifetime: SINGLETON - safe because:
/// - No instance state captured (except IHttpContextAccessor which is designed for this)
/// - All operations go through IHttpContextAccessor.HttpContext (per-request)
/// - Each request gets its own HttpContext automatically
/// 
/// Implements:
/// - IContextService: Apply/GetValue/GetService (per-request storage)
/// - IExecutionContextService: ExecutionId, timing, tracing
/// - IRequestContextService: HTTP-specific (headers, IP, claims, route)
/// </remarks>
public class RequestContextService : IRequestContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of <see cref="RequestContextService"/>.
    /// </summary>
    /// <param name="httpContextAccessor">The HTTP context accessor.</param>
    public RequestContextService(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    private HttpContext? Context => this._httpContextAccessor.HttpContext;

    // ========================================
    // IContextService - PER-REQUEST STORAGE
    // ========================================

    /// <inheritdoc />
    public void Apply(string key, object value)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentException("Key cannot be null or empty", nameof(key));
        }

        var context = this.Context;
        if (context == null)
        {
            throw new InvalidOperationException("HttpContext is not available. Ensure this is called within an HTTP request.");
        }

        context.Items[key] = value;
    }

    /// <inheritdoc />
    public object? GetValue(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentException("Key cannot be null or empty", nameof(key));
        }

        return this.Context?.Items.TryGetValue(key, out var value) == true ? value : null;
    }

    /// <inheritdoc />
    public T? GetValue<T>(string key)
    {
        var value = this.GetValue(key);
        if (value == null)
        {
            return default;
        }

        if (value is T typedValue)
        {
            return typedValue;
        }

        // Attempt conversion for value types
        try
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
        catch
        {
            return default;
        }
    }

    /// <inheritdoc />
    public TService GetService<TService>() where TService : class
    {
        var context = this.Context;
        if (context == null)
        {
            throw new InvalidOperationException("HttpContext is not available. Cannot resolve services outside an HTTP request.");
        }

        return context.RequestServices.GetRequiredService<TService>();
    }

    // ========================================
    // EXECUTION IDENTIFICATION (from IExecutionContextService)
    // ========================================

    /// <inheritdoc />
    public string ExecutionId => this.Context?.TraceIdentifier ?? Guid.NewGuid().ToString();

    /// <inheritdoc />
    public ExecutionType ExecutionType => ExecutionType.Request;

    /// <inheritdoc />
    public DateTime ExecutionStartTimeUtc => 
        this.GetValue<DateTime?>("__ExecutionStartTimeUtc") ?? DateTime.UtcNow;

    /// <inheritdoc />
    public TimeSpan ExecutionDuration => DateTime.UtcNow - this.ExecutionStartTimeUtc;

    // ========================================
    // CORRELATION (from IExecutionContextService)
    // ========================================

    /// <inheritdoc />
    public string? CorrelationId => this.GetHeader("X-Correlation-ID") ?? this.ExecutionId;

    /// <inheritdoc />
    public string? ParentExecutionId => this.GetHeader("X-Parent-Execution-ID");

    /// <inheritdoc />
    public string? TraceId => this.GetHeader("traceparent")?.Split('-').ElementAtOrDefault(1) 
        ?? this.GetHeader("X-Trace-ID");

    /// <inheritdoc />
    public string? SpanId => this.GetHeader("traceparent")?.Split('-').ElementAtOrDefault(2)
        ?? this.GetHeader("X-Span-ID");

    // ========================================
    // HTTP-SPECIFIC PROPERTIES
    // ========================================

    /// <inheritdoc />
    public string RequestPath => this.Context?.Request?.Path.Value ?? string.Empty;

    /// <inheritdoc />
    public string HttpMethod => this.Context?.Request?.Method ?? "UNKNOWN";

    /// <inheritdoc />
    public string? QueryString => this.Context?.Request?.QueryString.Value;

    /// <inheritdoc />
    public string IpAddress
    {
        get
        {
            // Check X-Forwarded-For header first (for proxied requests)
            var forwarded = this.GetHeader("X-Forwarded-For");
            if (!string.IsNullOrEmpty(forwarded))
            {
                // Take first IP if multiple
                return forwarded.Split(',')[0].Trim();
            }
            return this.Context?.Connection?.RemoteIpAddress?.ToString() ?? "unknown";
        }
    }

    /// <inheritdoc />
    public string? UserAgent => this.GetHeader("User-Agent");

    /// <inheritdoc />
    public string? Referer => this.GetHeader("Referer");

    /// <inheritdoc />
    public IReadOnlyDictionary<string, string> Headers =>
        this.Context?.Request?.Headers?.ToDictionary(
            h => h.Key,
            h => h.Value.ToString()) as IReadOnlyDictionary<string, string>
        ?? new Dictionary<string, string>();

    /// <inheritdoc />
    public string? GetHeader(string headerName)
    {
        if (this.Context?.Request?.Headers.TryGetValue(headerName, out var values) == true)
        {
            return values.FirstOrDefault();
        }
        return null;
    }

    /// <inheritdoc />
    public bool IsSecureConnection => this.Context?.Request?.IsHttps ?? false;

    /// <inheritdoc />
    public bool IsLocalRequest =>
        this.Context?.Connection?.RemoteIpAddress?.Equals(this.Context?.Connection?.LocalIpAddress) == true
        || this.IpAddress == "127.0.0.1"
        || this.IpAddress == "::1";

    // ========================================
    // AUTHENTICATION / IDENTITY
    // ========================================

    /// <inheritdoc />
    public ClaimsPrincipal? User => this.Context?.User;

    /// <inheritdoc />
    public bool IsAuthenticated => this.Context?.User?.Identity?.IsAuthenticated ?? false;

    /// <inheritdoc />
    public string? GetClaimValue(string claimType)
    {
        return this.Context?.User?.FindFirst(claimType)?.Value;
    }

    // ========================================
    // ROUTE DATA
    // ========================================

    /// <inheritdoc />
    public IReadOnlyDictionary<string, object?> RouteData =>
        this.Context?.Request?.RouteValues?.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value) as IReadOnlyDictionary<string, object?>
        ?? new Dictionary<string, object?>();

    /// <inheritdoc />
    public object? GetRouteValue(string key)
    {
        return this.Context?.Request?.RouteValues?.TryGetValue(key, out var value) == true ? value : null;
    }
}
