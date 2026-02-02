using App.Modules.Sys.Shared.Models;

namespace App.Modules.Sys.Shared.Models.Persistence
{
    /// <summary>
    /// <para>
    /// Does Implements: 
    /// <list type="bullet">
    /// <item><see cref="IHasGuidId"/></item>
    /// <item><see cref="IHasEnabled"/></item>
    /// <item><see cref="IHasTitleAndDescriptionAndImage"/></item>
    /// <item><see cref="IHasKeyGenericValue{T}"/></item>
    /// </list>
    /// </para>
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public interface IHasReferenceDataOfGuidIdEnabledTitleDescImageKeyGenValue<TValue> :
        IHasReferenceDataOfGuidIdEnabledTitleDescImgage,
        IHasKeyGenericValue<TValue>
        where TValue : struct //int
    {

    }
}
