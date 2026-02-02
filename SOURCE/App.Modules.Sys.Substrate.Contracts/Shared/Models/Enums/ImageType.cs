using System;
using System.Collections.Generic;
using System.Text;

namespace App.Modules.Sys.Shared.Models.Enums
{
    /// <summary>
    /// Enumeration of Image Types  
    /// </summary>
    public enum ImageType
    {
        /// <summary>
        /// Not set.
        /// </summary>
        Undefined=-1,

        /// <summary>
        /// Unknown.
        ///  </summary>
        Unknown=0,

        /// <summary>
        /// The image is a font.
        /// </summary>
        Font=1,
        
        /// <summary>
        /// The image is a url based image.
        /// </summary>
        Image=2

        // Svg?
    }
}
