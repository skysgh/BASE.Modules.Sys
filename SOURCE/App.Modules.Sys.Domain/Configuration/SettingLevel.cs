using System;

namespace App.Modules.Sys.Domain.Configuration
{
    /// <summary>
    /// Hierarchical settings levels.
    /// Each level can override settings from levels above.
    /// </summary>
    public enum SettingLevel
    {
        /// <summary>
        /// System-level settings (all workspaces, all users)
        /// Managed by system maintainer
        /// </summary>
        System = 0,

        /// <summary>
        /// Workspace-level settings (specific workspace, all users in workspace)
        /// Managed by workspace admin/account holder
        /// </summary>
        Workspace = 1,

        /// <summary>
        /// Person-level settings (specific user in specific workspace)
        /// Managed by individual user
        /// </summary>
        Person = 2
    }
}
