
namespace App.Modules.Core.Infrastructure.Models.Configuration._TOREVIEW
{
    /// <summary>
    /// ConfigurationSettings Object to configure a service,
    /// as per its name
    /// </summary>
    public class AzureCognitiveServicesComputerVisionConfigurationSettings
    {
        /// <summary>
        /// Gets or sets (from AppSettings)
        /// the ResourceName of this Computer Vision service.
        /// <para>
        /// <para>
        /// If not provided in AppSettings, using
        /// <c>ConfigurationKeys.AppCoreIntegrationAzureComputerVisionDefaultResourceName</c>
        /// falls back to 
        /// <c>Shared.Constants.ConfigurationKeys.AppCoreIntegrationAzureCommonResourceName</c>
        /// plus 'di'.
        /// </para>
        /// </para>
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSetting)]
        [Alias(Constants.ConfigurationKeys.AppCoreIntegrationAzureCognitiveServicesComputerVisionDefaultResourceName)]
        public string ResourceName
        {
            get; set;
        }

        /// <summary>
        /// THe confidential key required to access the service.
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.KeyVault)]
        [Alias(Constants.ConfigurationKeys.AppCoreIntegrationAzureCognitiveServicesComputerVisionDefaultAuthorizationKey)]
        public string Key
        {
            get; set;
        }

    }
}
