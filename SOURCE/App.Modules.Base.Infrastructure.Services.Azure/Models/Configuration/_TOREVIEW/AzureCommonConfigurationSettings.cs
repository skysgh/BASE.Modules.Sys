namespace App.Modules.Core.Infrastructure.Models.Configuration._TOREVIEW
{
    using App.Base.Shared.Attributes;

    /// <summary>
    /// A Configuration object appropriate for
    /// all the cloud services in general.
    /// <para>
    /// Specific configurations may then be needed
    /// by specific services. 
    /// </para>
    /// <para>
    /// In which case, they generally will be initialised
    /// with values from this common config object, and then
    /// if any specific values are found in configuration, 
    /// overridden.
    /// </para>
    /// </summary>
    public class AzureCommonConfigurationSettings : IHostSettingsBasedConfigurationObject
    {
        /// <summary>
        /// Gets or sets the root of the Cloud Service Resource Names
        /// (eg: something like 'corpappbranchenv')
        /// from AppSettings, 
        /// using the 
        /// <see cref="Constants.ConfigurationKeys.AppCoreIntegrationAzureCommonResourceName"/>
        /// AppSettings 
        /// key.
        /// <para>
        /// It's common for an app to name *most* resources with the 
        /// same name, as it gets hard to keep track of the exact way
        /// resource names were chosen. Also, different resources may
        /// have different restrictions (as in length or case allowed).
        /// </para>
        /// <para>
        /// When there are two of the same type of resource and therefore
        /// the same name can't be used for both, then the root or prefix
        /// is appended with a short tag of some kind (eg: 'corpappbranchenv'+'db1' 
        /// and 'corpappbranchenv' + 'db2') to differentiate between the two.
        /// </para>
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSettingsViaDeploymentPipeline)]
        [Alias(Constants.ConfigurationKeys.AppCoreIntegrationAzureCommonResourceName)]
        public string RootResourceName
        {
            get; set;
        }
    }
}