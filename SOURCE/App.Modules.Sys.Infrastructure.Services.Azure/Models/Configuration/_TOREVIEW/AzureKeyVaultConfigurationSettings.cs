namespace App.Modules.Core.Infrastructure.Models.Configuration._TOREVIEW
{
    using App.Base.Shared.Attributes;
    using App.Base.Shared.Constants;



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
        /// <see cref="Shared.Constants.ConfigurationKeys.AppCoreIntegrationAzureCommonResourceName"/>
        /// </para>
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSetting)]
        [Alias(Constants.ConfigurationKeys.AppCoreIntegrationAzureKeyVaultStoreResourceName)]
        public string ResourceName { get; set; }


        //[ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSetting)]
        //[Alias(Constants.ConfigurationKeys.AppCoreIntegrationAzureKeyVaultStoreSystemKeyPrefix)]
        //public string KeyPrefix
        //{
        //    get; set;
        //}
    }

}
