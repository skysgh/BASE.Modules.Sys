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
        /// 
        /// </summary>
        IEnumerable<IUniversalDisplayItemDisplayAction> AvailableActions { get; }
     
    }
}
