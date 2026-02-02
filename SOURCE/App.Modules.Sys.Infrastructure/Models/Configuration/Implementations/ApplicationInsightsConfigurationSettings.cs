using App.Modules.Sys.Infrastructure.Constants;
using App.Modules.Sys.Shared.Attributes;
using App.Modules.Sys.Substrate.Models.ConfigurationSettings;

namespace App.Modules.Sys.Infrastructure.Models.Configuration.Implementations
{
    /// <summary>
    /// An immutable host configuration object 
    /// describing the configuration of the 
    /// Application Insights service.
    /// </summary>
    public class ApplicationInsightsConfigurationSettings : IKeyVaultBasedConfigurationObject
    {

        /// <summary>
        /// The unique string key to identify app in Insights.
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.KeyVault)]
        [Alias(ConfigurationKeys.AppCoreIntegrationAzureApplicationInsightsInstrumentationKey)]
        public string? Key { get; set; }

        /// <summary>
        /// Get/Set whether to use the service.
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSetting)]
        [Alias(ConfigurationKeys.AppCoreIntegrationAzureApplicationInsightsInstrumentationKeyEnabled)]
        public bool Enabled { get; set; }
    }
}
