using System;
using System.Collections.Generic;
using System.Text;

namespace App.Modules.Sys.Shared.Models.Persistence
{
    /// <summary>
    /// Contract for start and end nullable Utc datetimes.
    /// </summary>
    public interface IHasStartAndEndUtcNullable
    {
        /// <summary>
        ///     Gets or sets the start datetime.
        /// </summary>
        DateTime? StartUtc { get; set; }

        /// <summary>
        /// Gets or sets the end datetime.
        /// </summary>
        DateTime? EndUtc { get; set; }
    }
}
