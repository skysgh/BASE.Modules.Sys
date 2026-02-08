namespace App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Constants;

/// <summary>
/// Table name constants for database schema.
/// Use these instead of magic strings in entity configurations.
/// </summary>
public static class DbSchemaTableNameConstants
{
    #region Reference Data Tables

    /// <summary>
    /// Table name for System Languages
    /// </summary>
    public const string Languages = "Languages";

    /// <summary>
    /// Table name for System Languages (alias for consistency)
    /// </summary>
    public const string SystemLanguages = "SystemLanguages";

    /// <summary>
    /// Languages associated to Workspaces
    /// </summary>
    public const string LanguagesToWorkspacesAssignments = "LanguagesToWorkspacesAssignments";

    /// <summary>
    /// Workspace Language Assignments
    /// </summary>
    public const string WorkspaceLanguageAssignments = "WorkspaceLanguageAssignments";

    #endregion

    #region Workspace Tables

    /// <summary>
    /// Table name for Workspaces (ie associated later to Accounts)
    /// </summary>
    public const string Workspaces = "Workspaces";

    /// <summary>
    /// Workspace members
    /// </summary>
    public const string WorkspaceMembers = "WorkspaceMembers";

    #endregion

    #region Configuration Tables

    /// <summary>
    /// Settings
    /// </summary>
    public const string Settings = "Settings";

    /// <summary>
    /// Setting values at different levels
    /// </summary>
    public const string SettingValues = "SettingValues";

    #endregion


    #region Session Tables

    /// <summary>
    /// Sessions
    /// </summary>
    public const string Sessions = "Sessions";

    /// <summary>
    /// Operations during a Session
    /// </summary>
    public const string SessionOperations = "SessionOperations";

    #endregion

    #region Identity Tables

    /// <summary>
    /// Table of Users
    /// </summary>
    public const string Users = "Users";

    /// <summary>
    /// Digital Identities that belong to User
    /// </summary>
    public const string DigitalIdentities = "DigitalIdentities";

    /// <summary>
    /// Association between Users and their n DigitalIdentities
    /// </summary>
    public const string DigitalIdentityToUserAssignments = "DigitalIdentityToUserAssignments";

    /// <summary>
    /// Providers of remote or local Identities
    /// </summary>
    public const string IdentityProviders = "IdentityProviders";

    #endregion

    #region Permission Tables

    /// <summary>
    /// System Permissions
    /// </summary>
    public const string Permissions = "Permissions";

    /// <summary>
    /// System Roles (collections of permissions)
    /// </summary>
    public const string Roles = "Roles";

    /// <summary>
    /// Association between Roles and Permissions
    /// </summary>
    public const string PermissionsToRoleAssignments = "PermissionsToRoleAssignments";

    /// <summary>
    /// Association between Roles and Users
    /// </summary>
    public const string RolesToUserAssignments = "RolesToUserAssignments";

    /// <summary>
    /// Association between Permissions and Users 
    /// (for direct permission grants or revocations, outside of roles)
    /// </summary>
    public const string PermissionsToUserAssignments = "PermissionsToUserAssignments";

    #endregion
}
