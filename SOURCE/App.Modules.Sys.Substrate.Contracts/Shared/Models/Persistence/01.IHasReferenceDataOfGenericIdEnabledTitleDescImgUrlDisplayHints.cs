using App.Modules.Sys.Shared.Models;

namespace App.Modules.Sys.Shared.Models.Persistence
{
    /// <summary>
    /// <para>
    /// Does NOT implement
    /// <list type="bullet">
    /// <item><see cref="IHasGuidId"/></item>
    /// </list>
    /// </para>
    /// <para>
    /// Does Implements 
    /// <list type="bullet">
    /// <item><see cref="IHasId{TId}"/></item>
    /// <item><see cref="IHasEnabled"/></item>
    /// <item><see cref="IHasTitleAndDescriptionAndImage"/></item>
    /// </list>
    /// </para>
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public interface IHasReferenceDataOfGenericIdEnabledTitleDescImage<TId> :
       IHasId<TId>,
       IHasTitleAndDescriptionAndImage
    {

    }

}