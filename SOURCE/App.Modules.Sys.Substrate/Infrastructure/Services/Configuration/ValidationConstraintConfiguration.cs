using App.Modules.Sys.Infrastructure.Domains.Configuration;
using System.Collections.Generic;

namespace App.Modules.Sys.Infrastructure.Services.Configuration;

/// <summary>
/// Configuration for validation constraints applied during reflection-based service discovery.
/// Defines which assemblies and namespaces are permitted or excluded during validation.
/// </summary>
/// <remarks>
/// Used by the module initialization system to filter discovered types during startup.
/// 
/// Use cases:
/// - Whitelist specific assemblies for validation (Permitted.AssemblyNames)
/// - Blacklist assemblies that should not be validated (Excluded.AssemblyNames)
/// - Control namespace-level inclusion/exclusion (Permitted/Excluded.Namespaces)
/// - Exclude types containing specific keywords (Excluded.Words)
/// 
/// Example configuration:
/// <code>
/// {
///   "ValidationConstraints": {
///     "Permitted": {
///       "AssemblyNames": ["App.Modules.Sys", "App.Modules.Core"],
///       "Namespaces": ["App.Modules"]
///     },
///     "Excluded": {
///       "AssemblyNames": ["System.*", "Microsoft.*"],
///       "Namespaces": ["Internal", "Test"],
///       "Words": ["Mock", "Fake", "Test"]
///     }
///   }
/// }
/// </code>
/// </remarks>
public class ValidationConstraintConfiguration : IServiceConfiguration
{
    /// <summary>
    /// Permitted validation targets.
    /// Assemblies and namespaces explicitly allowed for validation.
    /// </summary>
    /// <remarks>
    /// If specified, ONLY these assemblies/namespaces will be validated.
    /// Leave empty to allow all (unless excluded).
    /// </remarks>
    public Permitted Permitted { get; } = new Permitted();

    /// <summary>
    /// Excluded validation targets.
    /// Assemblies, namespaces, and keywords explicitly blocked from validation.
    /// </summary>
    /// <remarks>
    /// Exclusions take precedence over permissions.
    /// Use to filter out system assemblies, test code, or mock implementations.
    /// </remarks>
    public Excluded Excluded { get; } = new Excluded();
}

/// <summary>
/// Permitted validation targets (whitelist).
/// Defines which assemblies and namespaces are explicitly allowed for validation.
/// </summary>
/// <remarks>
/// If any items are specified, ONLY these targets will be validated.
/// Empty lists mean "allow all" (unless excluded).
/// 
/// Example:
/// - AssemblyNames: ["App.Modules.Sys"] → Only validate types in Sys module
/// - Namespaces: ["App.Modules"] → Only validate types in App.Modules namespace tree
/// </remarks>
public class Permitted
{
    /// <summary>
    /// Assembly names explicitly permitted for validation.
    /// Supports wildcards (e.g., "App.Modules.*").
    /// </summary>
    /// <remarks>
    /// Example: ["App.Modules.Sys", "App.Modules.Core"]
    /// </remarks>
    public List<string> AssemblyNames { get; } = [];

    /// <summary>
    /// Namespace prefixes explicitly permitted for validation.
    /// Supports hierarchical matching (e.g., "App.Modules" includes "App.Modules.Sys").
    /// </summary>
    /// <remarks>
    /// Example: ["App.Modules", "App.Base"]
    /// </remarks>
    public List<string> Namespaces { get; } = [];
}

/// <summary>
/// Excluded validation targets (blacklist).
/// Defines which assemblies, namespaces, and keywords are explicitly blocked from validation.
/// </summary>
/// <remarks>
/// Exclusions take precedence over permissions.
/// Use to filter out:
/// - System/framework assemblies (System.*, Microsoft.*)
/// - Test assemblies (*.Tests, *.UnitTests)
/// - Mock implementations (contains "Mock", "Fake")
/// - Internal/private namespaces
/// 
/// Example:
/// - AssemblyNames: ["System.*", "Microsoft.*"] → Skip all system assemblies
/// - Namespaces: ["Internal"] → Skip all internal namespaces
/// - Words: ["Mock", "Test"] → Skip types containing these keywords
/// </remarks>
public class Excluded
{
    /// <summary>
    /// Assembly names explicitly excluded from validation.
    /// Supports wildcards (e.g., "System.*" excludes all System assemblies).
    /// </summary>
    /// <remarks>
    /// Common exclusions:
    /// - "System.*" → All BCL assemblies
    /// - "Microsoft.*" → All Microsoft assemblies
    /// - "*.Tests" → All test assemblies
    /// - "netstandard" → .NET Standard reference assembly
    /// </remarks>
    public List<string> AssemblyNames { get; } = [];

    /// <summary>
    /// Namespace prefixes explicitly excluded from validation.
    /// Supports hierarchical matching (e.g., "Internal" excludes "Internal.*").
    /// </summary>
    /// <remarks>
    /// Common exclusions:
    /// - "Internal" → Internal implementation details
    /// - "Test" → Test helpers and fixtures
    /// - "Mocks" → Mock implementations
    /// </remarks>
    public List<string> Namespaces { get; } = [];

    /// <summary>
    /// Keywords that cause types to be excluded if found in their full name.
    /// Case-insensitive matching.
    /// </summary>
    /// <remarks>
    /// Common exclusions:
    /// - "Mock" → MockUserService, MockDbContext
    /// - "Fake" → FakeRepository, FakeService
    /// - "Test" → TestHelper, TestFixture
    /// - "Stub" → StubImplementation
    /// 
    /// Example: If "Mock" is in list, "App.Services.MockUserService" will be excluded.
    /// </remarks>
    public List<string> Words { get; } = [];
}

