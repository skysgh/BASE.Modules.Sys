using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Constants
{
    /// <summary>
    /// Collection of constants 
    /// used when defining db models
    /// </summary>
    public static class DbSchemaFieldSizeConstants
    {
        #region Raw Size Constants

        /// <summary>
        /// VeryTiny 
        /// Constant to define string lengths of 32 bytes
        /// </summary>
        public const int x32 = 32;

        /// <summary>
        /// x32+ 4 dash spaces for Guid length.
        /// </summary>
        public const int x36 = x32+4;

        /// <summary>
        /// Tiny
        /// Constant to define string lengths of 64 bytes
        /// </summary>
        public const int x64 = 64;

        /// <summary>
        /// VerySmall.
        /// Constant to define string lengths of 128 bytes
        /// </summary>
        public const int x128 = 128;

        /// <summary>
        /// Small (256)
        /// Constant to define string lengths of 256 bytes
        /// </summary>
        public const int x256 = 256;

        /// <summary>
        /// Half (512)
        /// Constant to define string lengths of 1024 bytes
        /// </summary>
        public const int x512 = 512;

        /// <summary>
        /// Default (1024)
        /// Constant to define string lengths of 1024 bytes
        /// </summary>
        public const int x1024 = 1024;

        /// <summary>
        /// Large (2048)
        /// Constant to define string lengths of 2048 bytes
        /// </summary>
        public const int x2048 = 2048;

        /// <summary>
        /// Very Large (4000)
        /// Constant to define string lengths of 4000 bytes
        /// <para>
        /// Which is the same length as NVARCHAR(Max)
        /// </para>
        /// </summary>
        public const int x4000 = 4000;

        #endregion

        #region Generic String Length Constants

        /// <summary>
        /// Constant to define string lengths of x128
        /// </summary>
        public const int StringLengthVerySmall = x128;

        /// <summary>
        /// Constant to define string lengths of x256
        /// </summary>
        public const int StringLengthSmall = x256;

        /// <summary>
        /// Constant to define string lengths of x1024
        /// </summary>
        public const int StringLengthMedium = x1024;

        /// <summary>
        /// Default string length = Medium = x1024
        /// </summary>
        public const int StringLengthDefault = StringLengthMedium;

        /// <summary>
        /// Constant to define string lengths of x2048
        /// </summary>
        public const int StringLengthLarge = x2048;

        /// <summary>
        /// Constant to define string lengths of x4000
        /// <para>
        /// Note that this is NOT the same as Max,
        /// which is much larger
        /// </para>
        /// </summary>
        public const int StringLengthVeryLarge = x4000;

        /// <summary>
        /// Constant to define string lengths of 2147483647
        /// <para>
        /// Which is equal to NVARCHAR(Max) </para>
        /// </summary>
        public const int StringLengthMax = 2147483647;

        /// <summary>
        /// Constant to define string length required to 
        /// encode Guids
        /// <para>
        /// TODO: Verify that if set to Unicode, doesn't truncate.
        /// </para>
        /// </summary>
        public const int GuidStringLength = x36;


        /// <summary>
        /// 
        /// </summary>
        public const int UserIdStringLength = GuidStringLength;

        #endregion

        #region Semantic Length Constants

        /// <summary>
        /// Standard length for short codes (e.g., language codes, country codes).
        /// (32)
        /// </summary>
        public const int CodeLength = x32;

        /// <summary>
        /// Standard length for titles and display names (128).
        /// </summary>
        public const int TitleLength = x128;

        /// <summary>
        /// Standard length for display hints/style hints (64).
        /// </summary>
        public const int DisplayHintLength = x64;

        /// <summary>
        /// Standard length for descriptions (multi-line text) (4000).
        /// </summary>
        public const int DescriptionLength = x4000;

        /// <summary>
        /// Standard length for string keys (64).
        /// </summary>
        public const int KeyLength = x64;

        /// <summary>
        /// Standard length for names (longer than titles) (128).
        /// </summary>
        public const int NameLength = x128;

        /// <summary>
        /// Standard length for URLs (x1024).
        /// </summary>
        public const int UrlLength = x1024;

        /// <summary>
        /// Standard default string length (256).
        /// </summary>
        public const int StringDefaultLength = x256;

        /// <summary>
        /// Standard length for tags (64).
        /// </summary>
        public const int TagLength = x64;

        #endregion
    }
}
