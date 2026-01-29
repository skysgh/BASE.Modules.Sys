namespace App.Modules.Base.Substrate.Models.Contracts
{
    /// <summary>
    /// Contract to apply a unique Key to an model.
    /// <para>
    /// Note: Consider the difference with <see cref="IHasName"/>.
    /// </para>
    /// </summary>
    public interface IHasKey
    {
        /// <summary>
        /// Get/Set the list item's unique key.
        /// <para>
        /// Note: Consider the difference with <see cref="IHasName"/>.
        /// </para>
        /// </summary>
        string Key { get; set; }
    }
}