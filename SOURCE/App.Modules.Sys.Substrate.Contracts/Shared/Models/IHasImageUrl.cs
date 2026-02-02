using System.ComponentModel;

namespace App.Modules.Sys.Shared.Models
{
    /// <summary>
    /// Url of an image to associate to this list item.
    /// <para>
    /// Prefer using <see cref="IHasImageUrlNullable"/>
    /// </para>
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IHasImageUrl
    {
        /// <summary>
        /// Url to an image
        /// </summary>
        string ImageUrl { get; set; }
    }

}