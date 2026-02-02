namespace App.Modules.Sys.Shared.Models.Enums
{
    /// <summary>
    /// Data classification levels for compliance and security.
    /// Used to mark sensitivity of data for GDPR, HIPAA, etc.
    /// </summary>
    public enum DataClassification
    {
        /// <summary>
        /// Public data - no restrictions
        /// </summary>
        Public = 0,

        /// <summary>
        /// Internal use only - not for external distribution
        /// </summary>
        Internal = 1,

        /// <summary>
        /// Confidential - requires authorization to access
        /// </summary>
        Confidential = 2,

        /// <summary>
        /// Restricted - highest security, audit logging required
        /// </summary>
        Restricted = 3,

        /// <summary>
        /// Personal Identifiable Information (PII) - GDPR/privacy laws apply
        /// </summary>
        PersonallyIdentifiable = 4
    }
}
