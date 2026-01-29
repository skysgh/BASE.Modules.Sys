using App.Base.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Base.Shared.Constants;

namespace App.Modules.Core.Infrastructure.Models.Configuration._TOREVIEW
{

    /// <summary>
    /// Configuration object to configure the Azure Redis Cache service.
    /// <para>
    /// TODO: Confirm Documentation
    /// </para>
    /// </summary>
    public class AzureRedisCacheConfigurationSettings
    {

        /// <summary>
        /// Gets or sets (from AppSettings)
        /// the ResourceName of this StorageAccount.
        /// <para>
        /// <para>
        /// If not provided in AppSettings, using
        /// <see cref="ConfigurationKeys.AppCoreIntegrationAzureStorageAccountDefaultResourceName"/>
        /// falls back to 
        /// <see cref="Shared.Constants.ConfigurationKeys.AppCoreIntegrationAzureCommonResourceName"/>
        /// plus 'di'.
        /// </para>
        /// </para>
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSetting)]
        [Alias(Constants.ConfigurationKeys.AppCoreIntegrationAzureRedisCacheResourceName)]
        public string ResourceName
        {
            get; set;
        }

        /// <summary>
        /// The confidential authorisation Key required to access the remote service.
        /// <para>
        /// Requires Confidential Storage.
        /// </para>
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.KeyVault)]
        [Alias(Constants.ConfigurationKeys.AppCoreIntegrationAzureRedisCacheDefaultAuthorizationKey)]
        public string Key
        {
            get; set;
        }

        /// <summary>
        /// Whether the service is enabled for use or not.
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSetting)]
        [Alias(Constants.ConfigurationKeys.AppCoreIntegrationAzureRedisEnabled)]
        public string Enabled
        {
            get; set;
        }
    }
}
