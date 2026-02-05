# Workspace Seed Data Implementation

## What Was Created

### ✅ Code-Based Seeding (Recommended)
**Files Modified:**
- `WorkspaceConfiguration.cs` - Added `SeedWorkspaces()` method
- `WorkspaceMemberConfiguration.cs` - Added `SeedWorkspaceMembers()` method

**Benefits:**
- ✅ Type-safe (compile-time checking)
- ✅ Version-controlled with migrations
- ✅ Automatic with database initialization
- ✅ No external dependencies

### 🔧 YAML-Based Seeding (Alternative)
**File Created:**
- `workspace-seed.yml` - External data file

**Benefits:**
- ✅ Non-technical users can edit
- ✅ Environment-specific data (dev/staging/prod)
- ✅ Can be loaded dynamically at runtime

---

## Seed Data Overview

### Three Workspaces:

| Workspace | ID | Tier | Users | Storage |
|-----------|----|----|-------|---------|
| **Default** | `10000000-...-001` | Free | 10 | 5GB |
| **Foo** | `10000000-...-002` | Pro | 50 | 100GB |
| **Bar** | `10000000-...-003` | Enterprise | Unlimited | Unlimited |

### User Memberships:

| User | Workspaces | Default | Roles |
|------|-----------|---------|-------|
| **Admin** (`20000000-...-001`) | All 3 | Default | Owner, Admin, Admin |
| **User1** (`20000000-...-002`) | Default, Foo | Default | Member, Member |
| **User2** (`20000000-...-003`) | Foo, Bar | Foo | Member, Guest |

---

## Next Steps

### To Apply Code-Based Seeds:
```bash
# Create migration with seed data
dotnet ef migrations add AddWorkspaceSeedData -p App.Modules.Sys.Infrastructure.Data.EF

# Apply to database
dotnet ef database update -p App.Modules.Sys.Infrastructure.Data.EF
```

### To Use YAML-Based Seeds (Requires Implementation):
1. Add YamlDotNet package
2. Create `IDataSeeder` service
3. Load YAML at startup
4. Insert if not exists

---

## Testing the Seeds

Once applied, you can test:

```csharp
// Should return 3 workspaces
var workspaces = await _workspaceRepository.GetByUserIdAsync(adminUserId);

// Admin should be in all 3 workspaces
var adminWorkspaces = await _workspaceService.GetUserWorkspacesAsync(adminUserId.ToString());
// Count: 3

// User1 should be in 2 workspaces
var user1Workspaces = await _workspaceService.GetUserWorkspacesAsync(user1Id.ToString());
// Count: 2

// Default workspace should be User1's default
var defaultWs = await _workspaceService.GetDefaultWorkspaceAsync(user1Id.ToString());
// defaultWs.Id == "10000000-0000-0000-0000-000000000001"
```

---

## GUID Conventions

**Workspace IDs:** `10000000-0000-0000-0000-00000000000X`  
**User IDs:** `20000000-0000-0000-0000-00000000000X`  
**Membership IDs:** `30000000-0000-0000-0000-00000000000X`

This makes seed data easily identifiable in the database.

---

## Recommendation

**Use code-based seeding** for:
- ✅ Core system data
- ✅ Test data in development
- ✅ Data that changes with schema

**Use YAML seeding** for:
- ✅ Environment-specific configuration
- ✅ Large datasets
- ✅ Data maintained by non-developers
