using App.Modules.Sys.Shared.Models;

namespace App.Modules.Sys.Shared.Models.Persistence
{
    /// <summary>
    /// Contract for a Displayable Reference Data option
    /// that implements 
    /// <para>
    /// Does NOT Implements: 
    /// <list type="bullet">
    /// <item><see cref="IHasKeyGenericValue{TValue}"/></item>
    /// </list>
    /// </para>
    /// <para>
    /// Does Implements: 
    /// <list type="bullet">
    /// <item><see cref="IHasGuidId"/></item>
    /// <item><see cref="IHasEnabled"/></item>
    /// <item><see cref="IHasTitleAndDescriptionAndImage"/></item>
    /// </list>
    /// </para>
    /// <para>
    /// But notice that it doesn't specify <c>Id</c>.
    /// </para>
    /// </summary>
    public interface IHasReferenceDataOfGuidIdEnabledTitleDescImgage :
        IHasReferenceDataOfGenericIdEnabledTitleDescImage<Guid>,
        IHasGuidId
    {

    }

}