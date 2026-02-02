using System;
using System.Collections.Generic;
using System.Text;

namespace App.Modules.Sys.Infrastructure.Azure.Models.Implementations.Base
{
    /// <summary>
    /// App-specific secret bundle - abstracts away Azure KeyVault's SecretBundle.
    /// No dependency on Microsoft.Azure.KeyVault types.
    /// </summary>
    public class AppSecretBundle
    {
        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string? Version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? ContentType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset? CreatedOn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset? UpdatedOn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset? ExpiresOn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> Tags { get; set; } = new();
    }
}