using Microsoft.Extensions.DependencyInjection;
using System;

namespace App.Modules.Sys.Infrastructure.Services.Extensions;

/// <summary>
/// DI registration extensions for service hierarchies.
/// Handles registration against multiple interfaces in inheritance chain.
/// </summary>
/// <remarks>
/// PROBLEM: When a service implements interface hierarchy, standard DI registration
/// only registers against the explicitly specified interface.
/// 
/// Example hierarchy:
/// IExecutionContextService (base)
///     ↓
/// IRequestContextService : IExecutionContextService
///     ↓
/// RequestContextService : IRequestContextService
/// 
/// BAD (only registers one interface):
/// services.AddScoped&lt;IRequestContextService, RequestContextService&gt;();
/// // IExecutionContextService NOT available for injection!
/// 
/// GOOD (registers all interfaces):
/// services.AddRequestContext&lt;RequestContextService&gt;();
/// // Both IExecutionContextService AND IRequestContextService available!
/// </remarks>
public static class ServiceRegistrationExtensions
{
    /// <summary>
    /// Register execution context service with proper interface hierarchy.
    /// Registers against IExecutionContextService for base access.
    /// </summary>
    /// <typeparam name="TImplementation">Concrete implementation type.</typeparam>
    /// <param name="services">Service collection.</param>
    /// <param name="lifetime">Service lifetime (default: Scoped).</param>
    /// <returns>Service collection for chaining.</returns>
    public static IServiceCollection AddExecutionContext<TImplementation>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TImplementation : class, IExecutionContextService
    {
        // Register concrete implementation
        services.Add(new ServiceDescriptor(typeof(TImplementation), typeof(TImplementation), lifetime));
        
        // Register against base interface (forwarding to concrete)
        services.Add(new ServiceDescriptor(
            typeof(IExecutionContextService),
            sp => sp.GetRequiredService<TImplementation>(),
            lifetime));

        return services;
    }

    /// <summary>
    /// Register request context service with proper interface hierarchy.
    /// Registers against BOTH IRequestContextService AND IExecutionContextService.
    /// </summary>
    /// <typeparam name="TImplementation">Concrete implementation type.</typeparam>
    /// <param name="services">Service collection.</param>
    /// <param name="lifetime">Service lifetime (default: Scoped).</param>
    /// <returns>Service collection for chaining.</returns>
    /// <remarks>
    /// Usage:
    /// services.AddRequestContext&lt;RequestContextService&gt;();
    /// 
    /// Now both injections work:
    /// - IExecutionContextService (base abstraction)
    /// - IRequestContextService (HTTP-specific)
    /// </remarks>
    public static IServiceCollection AddRequestContext<TImplementation>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TImplementation : class, IRequestContextService
    {
        // Register concrete implementation
        services.Add(new ServiceDescriptor(typeof(TImplementation), typeof(TImplementation), lifetime));
        
        // Register against IRequestContextService (forwarding to concrete)
        services.Add(new ServiceDescriptor(
            typeof(IRequestContextService),
            sp => sp.GetRequiredService<TImplementation>(),
            lifetime));
        
        // Register against base IExecutionContextService (forwarding to concrete)
        services.Add(new ServiceDescriptor(
            typeof(IExecutionContextService),
            sp => sp.GetRequiredService<TImplementation>(),
            lifetime));

        return services;
    }

    /// <summary>
    /// Register user context service (business context).
    /// </summary>
    /// <typeparam name="TImplementation">Concrete implementation type.</typeparam>
    /// <param name="services">Service collection.</param>
    /// <param name="lifetime">Service lifetime (default: Scoped).</param>
    /// <returns>Service collection for chaining.</returns>
    public static IServiceCollection AddUserContext<TImplementation>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TImplementation : class, IUserContextService
    {
        // Register concrete implementation
        services.Add(new ServiceDescriptor(typeof(TImplementation), typeof(TImplementation), lifetime));
        
        // Register against IUserContextService (forwarding to concrete)
        services.Add(new ServiceDescriptor(
            typeof(IUserContextService),
            sp => sp.GetRequiredService<TImplementation>(),
            lifetime));

        return services;
    }

    /// <summary>
    /// Register environment service (singleton - environment doesn't change).
    /// </summary>
    /// <typeparam name="TImplementation">Concrete implementation type.</typeparam>
    /// <param name="services">Service collection.</param>
    /// <returns>Service collection for chaining.</returns>
    public static IServiceCollection AddEnvironmentService<TImplementation>(
        this IServiceCollection services)
        where TImplementation : class, IEnvironmentService
    {
        // Singleton - environment doesn't change during runtime
        services.AddSingleton<TImplementation>();
        services.AddSingleton<IEnvironmentService>(sp => sp.GetRequiredService<TImplementation>());

        return services;
    }

    /// <summary>
    /// Register server device service (singleton - server hardware doesn't change).
    /// </summary>
    /// <typeparam name="TImplementation">Concrete implementation type.</typeparam>
    /// <param name="services">Service collection.</param>
    /// <returns>Service collection for chaining.</returns>
    public static IServiceCollection AddServerDeviceService<TImplementation>(
        this IServiceCollection services)
        where TImplementation : class, IServerDeviceService
    {
        // Singleton - server hardware doesn't change
        services.AddSingleton<TImplementation>();
        services.AddSingleton<IServerDeviceService>(sp => sp.GetRequiredService<TImplementation>());

        return services;
    }

    /// <summary>
    /// Register client device service (scoped - parsed from request User-Agent).
    /// </summary>
    /// <typeparam name="TImplementation">Concrete implementation type.</typeparam>
    /// <param name="services">Service collection.</param>
    /// <param name="lifetime">Service lifetime (default: Scoped).</param>
    /// <returns>Service collection for chaining.</returns>
    public static IServiceCollection AddClientDeviceService<TImplementation>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TImplementation : class, IClientDeviceService
    {
        // Scoped - device info parsed per request
        services.Add(new ServiceDescriptor(typeof(TImplementation), typeof(TImplementation), lifetime));
        services.Add(new ServiceDescriptor(
            typeof(IClientDeviceService),
            sp => sp.GetRequiredService<TImplementation>(),
            lifetime));

        return services;
    }
}
