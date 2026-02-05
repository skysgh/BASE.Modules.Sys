using App.Modules.Sys.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Modules.Sys.Shared.Models
{
    /// <summary>
    /// Universal contract for items displayed consistently across the UI.
    /// </summary>
    public interface IUniversalDisplayItem : 
        IHasTraceLevel,
        IHasTitleAndDescriptionAndImage, 
        IHasMetadata
    {
        /// <summary>
        /// Tags for categorizing/filtering this item.
        /// Examples: "Module", "Service", "Database", "Migration"
        /// UI can use these for filtering, grouping, and layout decisions.
        /// </summary>
        IEnumerable<string> Tags { get; }

        /// <summary>
        /// Available actions for this display item.
        /// </summary>
        IEnumerable<IUniversalDisplayItemDisplayAction> AvailableActions { get; }
     
    }
}
