namespace App.Modules.Base.Substrate.Models.ConfigurationSettings
{
    /// <summary>
    /// The contract for configuring cloud Storage 
    /// </summary>
    public interface IStorageAccountConfigurationSettings
    {
        /// <summary>
        /// The Resource Name
        /// </summary>
        string ResourceName { get; set; }

        /// <summary>
        /// The Suffix
        /// <para>
        /// TODO: Better documentation to example it
        /// </para>
        /// </summary>
        string ResourceNameSuffix { get; set; }

        /// <summary>
        /// TODO: Better docummentation
        /// </summary>
        string Key { get; set; }
    }
}