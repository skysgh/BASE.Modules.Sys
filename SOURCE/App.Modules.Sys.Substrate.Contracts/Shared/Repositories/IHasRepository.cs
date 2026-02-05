namespace App.Modules.Sys.Shared.Repositories;

/// <summary>
/// Marker interface for repository contracts.
/// All repository interfaces MUST implement this for architectural compliance.
/// </summary>
/// <remarks>
/// ARCHITECTURAL RULES:
/// 
/// Location Rules:
/// - Repository INTERFACES → Domain layer ONLY
/// - Repository IMPLEMENTATIONS → Infrastructure.Data.* layers ONLY
/// - Repository interfaces MUST be in *.Repositories namespace
/// 
/// Naming Convention:
/// - Interface: I{Entity}Repository (e.g., IUserRepository, ISettingRepository)
/// - Implementation: {Entity}Repository (e.g., UserRepository, SettingRepository)
/// 
/// Visibility Rules:
/// - Repository interfaces → public (consumed by Application layer)
/// - Repository implementations → internal (hidden from Application layer)
/// 
/// Dependency Flow:
/// Domain (repository interfaces) ← Infrastructure (repository implementations) ← Application (services)
/// 
/// Code Editor Rules (Future):
/// - Validate: All repository interfaces are in Domain layer
/// - Validate: All repository interfaces inherit from IHasRepository
/// - Validate: All repository implementations are internal
/// - Validate: No Application layer types depend on Infrastructure.Data.* directly
/// 
/// Examples:
/// ✅ CORRECT:
/// // Domain/Settings/Repositories/ISettingRepository.cs
/// public interface ISettingRepository : IHasRepository { ... }
/// 
/// // Infrastructure.Data.EF/Repositories/Settings/SettingRepository.cs
/// internal sealed class SettingRepository : ISettingRepository { ... }
/// 
/// // Application/Settings/Services/SettingsApplicationService.cs
/// public class SettingsApplicationService
/// {
///     private readonly ISettingRepository _repository; // ✅ Depends on interface (Domain)
/// }
/// 
/// ❌ INCORRECT:
/// // Application/Settings/Repositories/ISettingRepository.cs
/// public interface ISettingRepository { ... } // ❌ Wrong layer!
/// 
/// // Infrastructure.Data.EF/Repositories/SettingRepository.cs
/// public class SettingRepository { ... } // ❌ Should be internal!
/// 
/// // Application/Settings/Services/SettingsApplicationService.cs
/// public class SettingsApplicationService
/// {
///     private readonly SettingRepository _repository; // ❌ Depends on implementation!
/// }
/// </remarks>
public interface IHasRepository
{
    // Marker interface - no members
    // Used for:
    // 1. Discovery (find all repositories via reflection)
    // 2. Validation (enforce architectural rules)
    // 3. Documentation (self-documenting code)
}
