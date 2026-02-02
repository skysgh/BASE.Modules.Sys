namespace App.Modules.Sys.Shared.Models
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