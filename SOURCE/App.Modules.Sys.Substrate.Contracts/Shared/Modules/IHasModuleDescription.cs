using App.Modules.Sys.Shared.Models;

namespace App.Modules.Sys.Shared.Modules
{
    /// <summary>
    /// Contract for a Configuration object that 
    /// describes a Module.
    /// <para>
    /// Examples of use by Implementations thereof
    /// are for the configuration of Swagger
    /// at startup.
    /// </para>
    /// </summary>
    public interface IHasModuleDescription : IHasTitleAndDescription
    {
        /// <summary>
        /// Url to Organisation
        /// responsible for Module.
        /// </summary>
        string OrganisationUrl { get; }

        /// <summary>
        /// Url to Contact Information 
        /// regarding Organisation
        /// responsible for Module.
        /// </summary>
        string ContactUrl { get; }

    }
}
