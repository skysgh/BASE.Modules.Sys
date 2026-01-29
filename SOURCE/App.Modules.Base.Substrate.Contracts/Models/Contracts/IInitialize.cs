namespace App.Modules.Base.Substrate.Models.Contracts
{
    /// <summary>
    /// Contract for services and objects requiring initialisation.
    /// </summary>
    public interface IInitialize
    {
        /// <summary>
        /// Initialise!
        /// </summary>
        void Initialize();
    }
}