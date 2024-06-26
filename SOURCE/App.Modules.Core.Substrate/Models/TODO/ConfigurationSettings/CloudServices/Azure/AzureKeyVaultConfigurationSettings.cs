﻿namespace App.Modules.TmpSys.Shared.Models.TODO.ConfigurationSettings.CloudServices.Azure
{
    using App.Modules.TmpSys.Substrate.tmp.Attributes;
    using App.Modules.TmpSys.Substrate.tmp.Constants;
    using App.Modules.TmpSys.Shared.Models.TODO.ConfigurationSettings;



    /// <summary>
    /// An immutable host configuration object 
    /// describing the settings to access the 
    /// Azure Key Vault Service.
    /// </summary>
    public class AzureKeyVaultConfigurationSettings : IHostSettingsBasedConfigurationObject
    {


        /// <summary>
        /// Gets or sets the name of the System's KeyVault.
        /// <para>
        /// If not provided in AppSettings, using
        /// <see cref="ConfigurationKeys.AppCoreIntegrationAzureKeyVaultStoreResourceName"/>
        /// falls back to 
        /// <see cref="ConfigurationKeys.AppCoreIntegrationAzureCommonResourceName"/>
        /// </para>
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSetting)]
        [Alias(ConfigurationKeys.AppCoreIntegrationAzureKeyVaultStoreResourceName)]
        public string ResourceName { get; set; }


        //[ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSetting)]
        //[Alias(ConfigurationKeys.AppCoreIntegrationAzureKeyVaultStoreSystemKeyPrefix)]
        //public string KeyPrefix
        //{
        //    get; set;
        //}
    }

}
