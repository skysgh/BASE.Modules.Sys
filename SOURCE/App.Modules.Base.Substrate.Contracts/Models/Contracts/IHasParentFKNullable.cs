namespace App.Modules.Base.Substrate.Models.Contracts
{
    /// <summary>
    /// 
    /// <para>
    /// See <see cref="IHasParentFK"/>
    /// </para>
    /// </summary>
    public interface IHasParentFKNullable
    {

        /// <summary>
        /// The FK of the record's parent record -- if there is one.
        /// </summary>
        Guid? ParentFK { get; set; }
    }


}