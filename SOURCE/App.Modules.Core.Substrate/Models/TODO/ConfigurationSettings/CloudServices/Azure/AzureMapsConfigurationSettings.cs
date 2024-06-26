﻿using App.Modules.TmpSys.Substrate.tmp.Attributes;
using App.Modules.TmpSys.Substrate.tmp.Constants;

namespace App.Modules.TmpSys.Shared.Models.TODO.ConfigurationSettings.CloudServices.Azure
{
    /// <summary>
    /// Configuration object for using Azure Maps Service
    /// </summary>
    public class AzureMapsConfigurationSettings
    {
        /// <summary>
        /// Gets or sets (from AppSettings)
        /// the ResourceName of this Map service.
        /// <para>
        /// <para>
        /// If not provided in AppSettings, using
        /// <c>ConfigurationKeys.AppCoreIntegrationAzureMapsDefaultResourceName</c>
        /// falls back to 
        /// <c>Shared.Constants.ConfigurationKeys.AppCoreIntegrationAzureCommonResourceName</c>
        /// plus 'di'.
        /// </para>
        /// </para>
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSetting)]
        [Alias(ConfigurationKeys.AppCoreIntegrationAzureMapsDefaultResourceName)]
        public string ResourceName
        {
            get; set;
        }

        /// <summary>
        /// Gets the Credential Key required 
        /// to be provided to use the service.
        /// <para>
        /// Requires Confidential Storage.
        /// </para>
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.KeyVault)]
        [Alias(ConfigurationKeys.AppCoreIntegrationAzureMapsDefaultAuthorizationKey)]
        public string Key
        {
            get; set;
        }
    }
}
