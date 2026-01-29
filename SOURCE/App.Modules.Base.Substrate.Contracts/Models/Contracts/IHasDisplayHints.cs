namespace App.Modules.Base.Substrate.Models.Contracts
{
    /// <summary>
    /// Contract to add display Hinting
    /// information.
    /// <para>
    /// Implements
    /// <list type="bullet">
    /// <item><see cref="IHasDisplayOrderHint"/></item>
    /// <item><see cref="IHasDisplayStyleHintNullable"/></item>
    /// </list>
    /// </para>
    /// </summary>
    public interface IHasDisplayHints :
        IHasDisplayOrderHint,
        IHasDisplayStyleHintNullable
    { }

}