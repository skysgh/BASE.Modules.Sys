using System;
using System.Collections.Generic;
using System.Text;

namespace App.Modules.Sys.Shared.Models
{
    /// <summary>
    /// Contract for objects that have Service Credentials.
    /// </summary>
    public interface IHasServiceCredentials
    {
        /// <summary>
        /// THe 'name' of the Service acccount.
        /// </summary>
        string ClientId { get; set; }
        /// <summary>
        /// The 'password' of the Service account.
        /// </summary>
        string ClientSecret { get; set; }
    }
}
