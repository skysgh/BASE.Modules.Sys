# Cache Object Pattern

## Overview

The Cache Object Pattern provides a **self-contained, declarative approach to caching** where cache logic lives in specialized objects rather than being scattered throughout the codebase.

## Architecture

### Components

1. **`ICacheObject`** - Interface for self-contained cache objects
2. **`CacheObjectBase<T>`** - Base class for easy implementation
3. **`ICacheObjectRegistryService`** - Service that discovers and manages cache objects
4. **`CacheObjectRegistryService`** - Implementation with reflection-based discovery
5. **`ICacheService`** - Internal Tier 2 backing cache (hidden from application code)

### Design Principles

- **Encapsulation**: Cache objects know how to refresh themselves
- **Discovery**: Automatic registration via reflection at startup
- **Declarative**: Define key, duration, and refresh logic in one place
- **Separation**: Hide raw cache access behind the registry pattern
- **Type-Safety**: Generic access to cached values

## Creating a Cache Object

### Basic Example

```csharp
public class UserCountCacheObject : CacheObjectBase<int>
{
    public override string Key => "System.Metrics.UserCount";
    public override TimeSpan? Duration => TimeSpan.FromMinutes(5);

    protected override async Task<int> GetValueAsync(CancellationToken ct = default)
    {
        // Load from database, API, etc.
        return await _dbContext.Users.CountAsync(ct);
    }
}
```

### Static Data (Never Expires)

```csharp
public class CountryListCacheObject : CacheObjectBase<List<Country>>
{
    public override string Key => "System.Data.Countries";
    public override TimeSpan? Duration => null; // Never expires

    protected override Task<List<Country>> GetValueAsync(CancellationToken ct = default)
    {
        // Static reference data
        return Task.FromResult(LoadCountries());
    }
}
```

### With Dependency Injection

For cache objects that need services from DI:

```csharp
public class OrderStatsCacheObject : CacheObjectBase<OrderStats>
{
    private readonly IOrderRepository _orderRepo;

    public OrderStatsCacheObject(IOrderRepository orderRepo)
    {
        _orderRepo = orderRepo;
    }

    public override string Key => "Business.Metrics.OrderStats";
    public override TimeSpan? Duration => TimeSpan.FromMinutes(10);

    protected override async Task<OrderStats> GetValueAsync(CancellationToken ct = default)
    {
        return await _orderRepo.GetStatisticsAsync(ct);
    }
}
```

## Usage in Application Code

### Accessing Cached Values

```csharp
public class SomeService
{
    private readonly ICacheObjectRegistryService _cacheRegistry;

    public SomeService(ICacheObjectRegistryService cacheRegistry)
    {
        _cacheRegistry = cacheRegistry;
    }

    public async Task DoSomethingAsync()
    {
        // Get cached value - automatically refreshes if expired
        var userCount = await _cacheRegistry.GetValueAsync<int>(
            "System.Metrics.UserCount");

        var countries = await _cacheRegistry.GetValueAsync<List<Country>>(
            "System.Data.Countries");

        // Force refresh
        await _cacheRegistry.RefreshAsync("System.Metrics.UserCount");
    }
}
```

### Background Refresh

```csharp
public class CacheRefreshBackgroundService : BackgroundService
{
    private readonly ICacheObjectRegistryService _cacheRegistry;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Refresh all expired cache objects
            await _cacheRegistry.RefreshExpiredAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
```

## How It Works

### Startup (DoAfterBuild Phase)

```csharp
public override void DoAfterBuild()
{
    var cacheRegistry = ServiceProvider.GetRequiredService<ICacheObjectRegistryService>();
    
    if (cacheRegistry is CacheObjectRegistryService registry)
    {
        // Discovers all ICacheObject implementations via reflection
        // and registers them automatically
        registry.DiscoverAndRegisterAll(ServiceProvider);
    }
}
```

### Runtime Flow

1. **Request**: Code calls `GetValueAsync<T>("key")`
2. **Check Expiry**: Registry checks if cache object is expired
3. **Auto-Refresh**: If expired, calls object's `GetValueAsync()` method
4. **Return Value**: Returns cached (or refreshed) value
5. **Backing Store**: Optionally syncs to Tier 2 cache (Redis, etc.)

## Benefits

### Before (Traditional Approach)

```csharp
// Scattered throughout codebase
var userCount = await _cache.GetOrCreateAsync("user-count", async () =>
{
    return await _dbContext.Users.CountAsync();
}, TimeSpan.FromMinutes(5));
```

**Problems:**
- ? Cache logic duplicated everywhere
- ? No central management
- ? Hard to track what's cached
- ? Easy to forget refresh logic
- ? Tight coupling to cache service

### After (Cache Object Pattern)

```csharp
// One cache object definition
public class UserCountCacheObject : CacheObjectBase<int> { ... }

// Clean usage anywhere
var userCount = await _cacheRegistry.GetValueAsync<int>("System.Metrics.UserCount");
```

**Benefits:**
- ? Single source of truth per cached item
- ? Centralized discovery and management
- ? Self-contained refresh logic
- ? Type-safe access
- ? Decoupled from cache implementation

## Advanced Scenarios

### Conditional Refresh

```csharp
protected override async Task<T> GetValueAsync(CancellationToken ct = default)
{
    var data = await LoadDataAsync(ct);
    
    if (data.RequiresValidation)
    {
        data = await ValidateAndEnrichAsync(data, ct);
    }
    
    return data;
}
```

### Cache Warming

```csharp
public override void DoAfterBuild()
{
    var cacheRegistry = ServiceProvider.GetRequiredService<ICacheObjectRegistryService>();
    
    // Warm critical caches immediately
    Task.Run(async () =>
    {
        await cacheRegistry.GetValueAsync<List<Country>>("System.Data.Countries");
        await cacheRegistry.GetValueAsync<AppConfig>("System.Configuration");
    });
}
```

### Monitoring

```csharp
public async Task GetCacheStatusAsync()
{
    var registry = ServiceProvider.GetRequiredService<ICacheObjectRegistry>();
    
    var keys = registry.GetKeys();
    
    foreach (var key in keys)
    {
        var exists = registry.Contains(key);
        Console.WriteLine($"Cache: {key} - Exists: {exists}");
    }
}
```

## Migration Guide

### Old Pattern (CacheRegistryService)

```csharp
var value = await _cacheRegistry.GetOrCreateAsync(
    "my-key",
    async ct => await LoadDataAsync(),
    TimeSpan.FromMinutes(10));
```

### New Pattern (Cache Objects)

1. Create cache object:
```csharp
public class MyDataCacheObject : CacheObjectBase<MyData>
{
    public override string Key => "my-key";
    public override TimeSpan? Duration => TimeSpan.FromMinutes(10);
    
    protected override Task<MyData> GetValueAsync(CancellationToken ct = default)
        => LoadDataAsync();
}
```

2. Use in code:
```csharp
var value = await _cacheRegistry.GetValueAsync<MyData>("my-key");
```

## Best Practices

1. **Naming Convention**: Use hierarchical keys: `"System.Metrics.UserCount"`, `"Business.Orders.Stats"`
2. **Reasonable Durations**: Balance freshness vs. load (typically 5-60 minutes)
3. **Null Handling**: Return sensible defaults if data load fails
4. **Logging**: Use ILogger in GetValueAsync for debugging
5. **Testing**: Cache objects are easy to unit test in isolation

## Testing Cache Objects

```csharp
[Fact]
public async Task UserCountCacheObject_RefreshesCorrectly()
{
    // Arrange
    var cacheObject = new UserCountCacheObject(_mockRepo.Object);
    
    // Act
    await cacheObject.RefreshAsync();
    var value = await cacheObject.GetAsync();
    
    // Assert
    Assert.Equal(expectedCount, value);
    Assert.False(cacheObject.IsExpired);
}
```

## Summary

The Cache Object Pattern provides:
- **Clean separation** between cache definition and usage
- **Automatic discovery** via reflection
- **Self-refreshing** behavior built-in
- **Type-safe** access patterns
- **Centralized management** of all cached data

This makes caching **declarative, discoverable, and maintainable**.
