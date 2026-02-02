using App.Modules.Sys.Infrastructure.Configuration;
using App.Modules.Sys.Infrastructure.Constants;
using App.Modules.Sys.Shared.Attributes;

namespace App.Modules.Sys.Infrastructure.Models.Configuration.Implementations
{
    /// <summary>
    /// Configuration Settings for configuring the GeoIPService
    /// </summary>
    public class GeoIPServiceConfigurationSettings : IServiceConfiguration
    {
        /// <summary>
        /// The Service Credentials key
        /// <para>
        /// Make sure this kind of secrets are not gotten from AppSettings.
        /// </para>
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSettingsViaDeploymentPipeline)]
        [Alias(ConfigurationKeys.AppCoreIntegrationGeoIPServiceClientId)]
        public string? ClientId
        {
            get; set;
        }

        /// <summary>
        /// The Service Credentials Secret
        /// <para>
        /// Make sure this kind of secrets are not gotten from AppSettings.
        /// </para>
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.KeyVault)]
        [Alias(ConfigurationKeys.AppCoreIntegrationGeoIPServiceClientSecret)]
        public string? Secret
        {
            get; set;
        }


        /// <summary>
        /// Url of the remote service.
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSettingsViaDeploymentPipeline)]
        [Alias(ConfigurationKeys.AppCoreIntegrationGeoIPServiceBaseUri)]
        public string? BaseUri
        {
            get; set;
        }


        /// <summary>
        /// Misc configuration that can be used to configure the service.
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSettingsViaDeploymentPipeline)]
        [Alias(ConfigurationKeys.AppCoreIntegrationGeoIPServiceClientMiscConfig)]
        public string? MiscConfig
        {
            get; set;
        }
    }
}
