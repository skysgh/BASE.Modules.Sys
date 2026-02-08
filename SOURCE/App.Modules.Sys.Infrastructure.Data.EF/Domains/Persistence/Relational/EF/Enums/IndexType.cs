namespace App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Enums;

/// <summary>
/// Specifies the type of database index to create.
/// </summary>
public enum IndexType
{
    /// <summary>
    /// No index.
    /// </summary>
    None,

    /// <summary>
    /// Unique index - no duplicate values allowed.
    /// </summary>
    Unique,

    /// <summary>
    /// Non-unique index - duplicate values allowed.
    /// </summary>
    NonUnique
}
