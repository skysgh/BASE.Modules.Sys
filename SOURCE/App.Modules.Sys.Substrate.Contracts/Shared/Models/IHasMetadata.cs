using System;
using System.Collections.Generic;
using System.Text;

namespace App.Modules.Sys.Shared.Models
{
    /// <summary>
    /// Contract for items backed by key-value pairs.
    /// </summary>
    public interface IHasMetadata
    {
        /// <summary>
        /// Metadata key-value pairs.
        /// </summary>
        IDictionary<string, object> Metadata { get; }

    }
}
