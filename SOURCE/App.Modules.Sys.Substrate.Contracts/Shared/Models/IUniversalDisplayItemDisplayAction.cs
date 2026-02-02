namespace App.Modules.Sys.Shared.Models
{
    /// <summary>
    /// Contract for items displayed consistently across the UI.
    /// </summary>

    public interface IUniversalDisplayItemDisplayAction:
        IHasTitleAndDescriptionAndImage,
        IHasActionKey,
        IHasMetadata
    {

    }
}
