using System;

namespace App.Modules.Sys.Domain.Domains.Configuration
{
    /// <summary>
    /// Hierarchical settings levels (5-tier architecture).
    /// Each level can override settings from levels above it.
    /// Lower numbers = higher priority (more specific).
    /// 
    /// Resolution order (most specific first):
    /// 1. User (individual preferences)
    /// 2. Workspace (tenant/organization)
    /// 3. Distributor (regional partner - CRITICAL for data sovereignty)
    /// 4. Provider (platform operator/hosting company)
    /// 5. Developer (software defaults)
    /// 
    /// See ADR-001 for rationale on why 5 tiers (not 3).
    /// </summary>
    public enum SettingLevel
    {
        /// <summary>
        /// Developer-level settings (software defaults).
        /// Managed by: Development team (you).
        /// Example: Maximum password length, default theme.
        /// </summary>
        Developer = 0,

        /// <summary>
        /// Provider-level settings (platform operator).
        /// Managed by: Platform hosting company.
        /// Example: Platform branding, support contact info.
        /// </summary>
        Provider = 1,

        /// <summary>
        /// Distributor-level settings (regional partner/licensee).
        /// Managed by: Regional distributor (e.g., BASE France, BASE Germany).
        /// Example: Regional data residency, local support channels.
        /// CRITICAL for data sovereignty and compliance (GDPR, etc.).
        /// </summary>
        Distributor = 2,

        /// <summary>
        /// Workspace-level settings (tenant/organization).
        /// Managed by: Workspace admin/account holder.
        /// Example: Company theme, workspace features, user limits.
        /// </summary>
        Workspace = 3,

        /// <summary>
        /// User-level settings (individual preferences).
        /// Managed by: Individual user.
        /// Example: Personal theme, language, UI density.
        /// </summary>
        User = 4
    }
}
