using System;
using System.Collections.Generic;
using System.Text;

namespace App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Constants
{
    /// <summary>
/// Database schema name constants for organizing tables into logical groups.
/// </summary>
public static class DbSchemaSchemaNameConstants
    {
        /// <summary>
        /// Default schema for this module (same as module key).
        /// </summary>
        public const string Default = Substrate.ModuleConstants.DbSchemaKey;

        /// <summary>
        /// Schema for reference/lookup data tables.
        /// </summary>
        public const string ReferenceData = "refdata";

        /// <summary>
        /// Schema for audit/history tables.
        /// </summary>
        public const string Audit = "audit";

        /// <summary>
        /// Schema for system configuration tables.
        /// </summary>
        public const string Configuration = "config";

        /// <summary>
        /// Schema for identity/security tables.
        /// </summary>
        public const string Identity = "identity";

        /// <summary>
        /// Schema for session management tables.
        /// </summary>
        public const string Sessions = "sessions";

        /// <summary>
        /// Schema for workspace-related tables.
        /// </summary>
        public const string Workspaces = "workspace";


    }
}
