using App.Modules.Sys.Shared.Models.Persistence;

namespace App.Modules.Sys.Shared.Models
{
    /// <summary>
    /// Contract for entities that has child records of some kind.
    /// <para>
    /// TODO: Provide example for documentation.
    /// </para>
    /// <para>
    /// There is no equivalent 
    /// <c>IHasChildrenNullable{T}</c>
    /// as the property should always provide
    /// at least an empty array.
    /// </para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHasChildren<T> where T : IHasGuidId
    {
    }
}