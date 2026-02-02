namespace App.Modules.Sys.Shared.Models
{
    /// <summary>
    /// Contract for objects that have Title, Description, Image URL, and Display Hints.
    /// Used for navigation items, menu items, and other UI display elements.
    /// </summary>
    public interface IHasTitleAndDescriptionAndImageUrlAndDisplayHints :
        IHasTitle,
        IHasDescription,
        IHasImageUrl,
        IHasDisplayStyleHintNullable
    {
        // Composite interface combining:
        // - string Title { get; set; }                 // from IHasTitle
        // - string Description { get; set; }           // from IHasDescription
        // - string? ImageUrl { get; set; }             // from IHasImageUrl
        // - string? DisplayStyleHint { get; set; }     // from IHasDisplayStyleHintNullable
    }
}
