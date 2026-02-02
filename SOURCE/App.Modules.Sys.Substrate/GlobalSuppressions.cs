// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

// Assembly-level suppressions for legacy _TOREVIEW namespace pattern
[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", 
    Scope = "namespaceanddescendants", 
    Target = "~N:App.Modules.Sys.Shared.Models.Messages._TOREVIEW",
    Justification = "_TOREVIEW is legacy namespace for code under review - will be refactored")]

// Specific type with underscore in name for EF mapping
[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", 
    Scope = "type", 
    Target = "~T:App.Modules.Sys.Shared.Models.Messages._TOREVIEW.Entities.TenancySpecific.PrincipalSecurityProfile_Permission_Assignment",
    Justification = "Legacy EF entity with underscore in name - database mapping constraint")]

[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores",
    Scope = "type",
    Target = "~T:App.Modules.Sys.Shared.Models.Messages._TOREVIEW.Entities.TenancySpecific.PrincipalSecurityProfileRole_Permission_Assignment",
    Justification = "Legacy EF entity with underscore in name - database mapping constraint")]


