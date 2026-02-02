namespace App.Modules.Sys.Shared.Models
{

    /// <summary>
    /// Contract for entities that have an image as a font or image.
    /// </summary>
    public interface IHasImageStyleNullable
    {


        /// <summary>
        /// Style to either apply to an image, or be an image (from a font pack) if no image url provided.
        /// </summary>
        string? ImageStyle { get; }
    }
}