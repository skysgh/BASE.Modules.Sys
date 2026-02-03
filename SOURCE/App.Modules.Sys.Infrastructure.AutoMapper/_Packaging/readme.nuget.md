# App.Modules.Sys.Infrastructure.AutoMapper

**Plugin architecture for object mapping using AutoMapper.**

## Purpose

This assembly provides an **optional** AutoMapper implementation of `IObjectMappingService`. It is the **ONLY** place in the entire solution that references AutoMapper.

## Key Design Principles

### 1. Plugin Architecture
- Can be **swapped** with alternative implementations (Mapster, manual mapping, etc.)
- No other code knows about AutoMapper
- Everything uses `IObjectMappingService` abstraction

### 2. Zero Coupling
```
Application Layer:
  - Defines ObjectMapBase<TFrom, TTo> mappers
  - NO AutoMapper dependency

Infrastructure.AutoMapper:
  - Discovers ObjectMapBase implementations
  - Registers with AutoMapper
  - Implements IObjectMappingService

Rest of Solution:
  - Uses IObjectMappingService
  - NO AutoMapper dependency
```

### 3. Easy to Swap
```csharp
// Option A: Use AutoMapper
services.AddAutoMapperObjectMapping();

// Option B: Use Mapster (future)
services.AddMapsterObjectMapping();

// Option C: Use manual mapping (future)
services.AddManualObjectMapping();
```

Application code stays **exactly the same** - just change DI registration.

---

## Usage

### Register in Startup
```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Add AutoMapper plugin
        services.AddAutoMapperObjectMapping();
        
        // Or specify assemblies explicitly
        services.AddAutoMapperObjectMapping(
            typeof(UserViewModel).Assembly);
    }
}
```

### Use in Code
```csharp
public class UserManagementService
{
    private readonly IObjectMappingService _mapper;
    
    public UserManagementService(IObjectMappingService mapper)
    {
        _mapper = mapper;
    }
    
    public async Task<UserViewModel> GetUserAsync(Guid id)
    {
        var user = await _repository.GetByIdAsync(id);
        return _mapper.Map<User, UserViewModel>(user);
    }
}
```

**Note:** Code has NO idea it's using AutoMapper - it only knows `IObjectMappingService`.

---

## How It Works

### 1. Discovery
```csharp
// Scans Application assemblies for:
public class UserToUserViewModelMap : ObjectMapBase<User, UserViewModel>
{
    public override void Configure(dynamic config)
    {
        // Marker class - AutoMapper does actual mapping
    }
}
```

### 2. Registration
```csharp
// AutoMapperObjectMappingService:
// - Discovers all ObjectMapBase<,> implementations
// - Creates AutoMapper CreateMap<TFrom, TTo>() for each
// - Builds IMapper instance
```

### 3. Runtime
```csharp
// Caller uses abstraction:
var vm = _mapper.Map<User, UserViewModel>(user);

// Internally delegates to AutoMapper:
return _autoMapper.Map<User, UserViewModel>(user);
```

---

## Alternative Implementations

### Mapster Plugin (Future)
```
App.Modules.Sys.Infrastructure.Mapster/
??? Services/
?   ??? MapsterObjectMappingService.cs
??? Extensions/
    ??? ServiceCollectionExtensions.cs

// Usage:
services.AddMapsterObjectMapping();
```

### Manual Mapping Plugin (Future)
```
App.Modules.Sys.Infrastructure.ManualMapper/
??? Services/
?   ??? ManualObjectMappingService.cs
??? Extensions/
    ??? ServiceCollectionExtensions.cs

// Usage:
services.AddManualObjectMapping();
```

**All three plugins work with the SAME ObjectMapBase<,> definitions!**

---

## Benefits

### For Developers Who Love AutoMapper
- ? Use AutoMapper
- ? Full AutoMapper features available
- ? Performance optimized

### For Developers Who Hate AutoMapper
- ? Swap to manual mapping
- ? Zero "magic"
- ? **NO code changes** - just change DI registration

### For Performance-Critical Apps
- ? Benchmark different implementations
- ? Choose fastest
- ? Application code unchanged

### For Testing
- ? Mock `IObjectMappingService`
- ? No AutoMapper needed in tests
- ? Simple, fast unit tests

---

## Architecture Diagram

```
???????????????????????????????????????????
?  Application Layer                      ?
?  - UserToUserViewModelMap              ?
?    (ObjectMapBase<User, UserViewModel>) ?
?  - NO AutoMapper dependency             ?
???????????????????????????????????????????
               ? implements
               ?
???????????????????????????????????????????
?  Substrate.Contracts                    ?
?  - ObjectMapBase<TFrom, TTo>           ?
?  - IObjectMappingService                ?
???????????????????????????????????????????
               ? implemented by
               ?
???????????????????????????????????????????
?  Infrastructure.AutoMapper (Plugin)     ?  ? THIS ASSEMBLY
?  - AutoMapperObjectMappingService       ?
?  - ONLY AutoMapper dependency           ?
???????????????????????????????????????????
               ? registers
               ?
???????????????????????????????????????????
?  DI Container                           ?
?  IObjectMappingService ? AutoMapper     ?
???????????????????????????????????????????
```

---

## Dependencies

**This assembly:**
- ? AutoMapper 13.0.1
- ? AutoMapper.Extensions.Microsoft.DependencyInjection 12.0.1

**Rest of solution:**
- ? NO AutoMapper references
- ? Only `IObjectMappingService` abstraction

**To remove AutoMapper from entire solution:**
1. Delete this assembly
2. Add alternative plugin (Mapster, ManualMapper)
3. Update DI registration
4. **Done** - no other changes needed

---

## Maintenance

**When to update this assembly:**
- AutoMapper releases new version
- Performance improvements available
- Bug fixes in AutoMapper

**What stays unchanged:**
- Application layer mappers
- IObjectMappingService contract
- All consuming code

**Clean separation = low maintenance burden.**

