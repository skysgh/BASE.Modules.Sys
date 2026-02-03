using System.Collections.Generic;

namespace App.Modules.Sys.Infrastructure.Web.Routing
{
    /// <summary>
    /// Configuration for workspace routing.
    /// Defines which words are reserved (cannot be workspace IDs).
    /// </summary>
    public class WorkspaceRoutingOptions
    {
        /// <summary>
        /// Reserved words that cannot be workspace IDs.
        /// These words in first segment indicate it's NOT a workspace.
        /// </summary>
        public HashSet<string> ReservedWords { get; set; } = new(new[]
        {
            "api",       // API endpoints
            "odata",     // ODATA endpoints
            "graphql",   // GraphQL endpoints
            "app",       // Application routes
            "admin",     // Admin panel
            "public",    // Public resources
            "health",    // Health checks
            "swagger",   // Swagger UI
            "_",         // Internal routes
            "account"    // Account management
        });

        /// <summary>
        /// Default workspace when none specified.
        /// </summary>
        public string DefaultWorkspace { get; set; } = "default";

        /// <summary>
        /// Whether to allow subdomain-based workspace detection.
        /// If true, extracts first segment of host and validates against database.
        /// </summary>
        public bool AllowSubdomainWorkspaces { get; set; } = true;

        /// <summary>
        /// Whether to allow path-based workspace detection.
        /// If true, checks first path segment against database.
        /// </summary>
        public bool AllowPathWorkspaces { get; set; } = true;

        /// <summary>
        /// Check if a word is reserved.
        /// </summary>
        public bool IsReserved(string word)
        {
            return ReservedWords.Contains(word.ToLowerInvariant());
        }
    }
}

