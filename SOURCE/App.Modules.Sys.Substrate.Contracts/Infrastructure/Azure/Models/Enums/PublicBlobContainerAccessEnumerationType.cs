using System;
using System.Collections.Generic;
using System.Text;

namespace App.Modules.Sys.Infrastructure.Azure.Models.Enums
{
    /// <summary>
    /// An enumeration of remote blob container access types.
    /// </summary>
    public enum PublicBlobContainerAccessEnumerationType
    {
        /// <summary>
        /// Not Set.
        /// </summary>
        Undefined=0,



        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown=1,


        /// <summary>
        /// Object Storage
        /// </summary>
        Blobs=2
    }
}
