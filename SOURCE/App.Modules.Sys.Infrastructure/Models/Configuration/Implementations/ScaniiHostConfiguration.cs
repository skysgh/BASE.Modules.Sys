using App.Modules.Sys.Infrastructure.Constants;
using App.Modules.Sys.Shared.Attributes;
using App.Modules.Sys.Substrate.Models.ConfigurationSettings;

namespace App.Modules.Sys.Infrastructure.Models.Configuration.Implementations
{
    /// <summary>
    /// Configurer for the <c>IScaniiMalwareService</c>
    /// </summary>
    /// <remarks>
    /// <para>
    /// Implementations of 
    /// <c>IServiceConfigurer</c>
    /// are used to configure implementations of <c>IService</c>.
    /// </para>
    /// <para>
    /// They may in turn invoke an <c>IService</c> implementation
    /// to hydrate implmentations of 
    /// <see cref="IHostSettingsBasedConfigurationObject"/></para>
    /// </remarks>
    public class ScaniiHostConfiguration : IHostSettingsBasedConfigurationObject
    {
        /// <summary>
        /// The Service Credential Key 
        /// <para>
        /// Make sure this kind of secrets are not gotten from AppSettings.
        /// </para>
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSettingsViaDeploymentPipeline)]
        [Alias(ConfigurationKeys.AppCoreIntegrationMalwareDetectionClientId)]
        public string? Key
        {
            get; set;
        }

        /// <summary>
        /// The Service Credential Secret
        /// <para>
        /// Make sure this kind of secrets are not gotten from AppSettings.
        /// </para>
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.KeyVault)]
        [Alias(ConfigurationKeys.AppCoreIntegrationMalwareDetectionClientSecret)]
        public string? Secret
        {
            get; set;
        }

        /// <summary>
        /// Base Url of Service
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSettingsViaDeploymentPipeline)]
        [Alias(ConfigurationKeys.AppCoreIntegrationMalwareDetectionBaseUri)]
        public string? BaseUri
        {
            get; set;
        }


        /// <summary>
        /// Misc Config. Handler expected to know how to use.
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSettingsViaDeploymentPipeline)]
        [Alias(ConfigurationKeys.AppCoreIntegrationMalwareDetectionClientMiscConfig)]
        public string? MiscConfig
        {
            get; set;
        }
    }
}