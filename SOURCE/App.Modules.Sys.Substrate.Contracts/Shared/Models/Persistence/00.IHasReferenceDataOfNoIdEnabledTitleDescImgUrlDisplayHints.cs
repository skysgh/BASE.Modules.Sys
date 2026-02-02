using App.Modules.Sys.Shared.Models;

namespace App.Modules.Sys.Shared.Models.Persistence
{
    /// <summary>
    /// Contract for Reference data
    /// that is not intended to be displayed 
    /// to end users (ie, it's a sparse version
    /// of the more polished 
    /// <para>
    /// Does NOT implement:
    /// <list type="bullet">
    /// <item><see cref="IHasGuidId"/></item>
    /// </list>
    /// </para>
    /// <para>
    /// does implements:
    /// <list type="bullet">
    /// <item><see cref="IHasEnabled"/></item>
    /// <item><see cref="IHasTitleAndDescriptionAndImage"/></item>
    /// </list>
    /// </para>
    /// </summary>
    public interface IHasReferenceDataOfNoIdEnabledTitleDescImgage :
        IHasEnabled,
        IHasTitleAndDescriptionAndImage
    {

    }

}