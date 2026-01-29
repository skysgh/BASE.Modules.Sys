
namespace App.Modules.Core.Infrastructure.Models.Configuration._TOREVIEW
{
    using App.Base.Shared.Attributes;
    using App.Base.Shared.Constants;

    /// <summary>
    /// An immutable host configuration object 
    /// describing the configuration needed to 
    /// access the system's
    /// Azure Storage Account Service to access its 
    /// Containers
    /// </summary>
    public class AzureStorageAccountDefaultConfigurationSettings : IKeyVaultBasedConfigurationObject, IStorageAccountConfigurationSettings
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
        [Alias(Constants.ConfigurationKeys.AppCoreIntegrationAzureStorageAccountDefaultResourceName)]
        public string ResourceName
        {
            get; set;
        }


        /// <summary>
        /// The default name for resources.
        /// <para>
        /// TODO: Confirm Documentation
        /// </para>
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSetting)]
        [Alias(Constants.ConfigurationKeys.AppCoreIntegrationAzureCommonResourceName)]
        public string DefaultResourceName
        {
            get; set;
        }



        /// <summary>
        /// Gets or sets (from AppSettings)
        /// the ResourceName Suffix of this StorageAccount.
        /// <para>
        /// <para>
        /// Default Value is 'm1'.
        /// </para>
        /// <para>
        /// The value is appended to <see cref="ResourceName"/>.
        /// </para>
        /// <para>
        /// Can be overridden (or cleared) using 
        /// <see cref="ConfigurationKeys.AppCoreIntegrationAzureStorageAccountDefaultResourceNameSuffix"/>,
        /// in AppSettings.
        /// </para>
        /// </para>
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSetting)]
        [Alias(Constants.ConfigurationKeys.AppCoreIntegrationAzureStorageAccountDefaultResourceNameSuffix)]
        public string ResourceNameSuffix
        {
            get; set;
        }


        /// <summary>
        /// Gets or sets 
        /// (from the KeyVault) 
        /// the Key for the ServiceAccount.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.KeyVault)]
        [Alias(Constants.ConfigurationKeys.AppCoreIntegrationAzureStorageAccountDefaultKey)]
        public string Key
        {
            get; set;
        }

        /// <summary>
        /// The optional override for the ConnectionString if it needs to be set by 
        /// hand.
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.KeyVault)]
        [Alias(Constants.ConfigurationKeys.AppCoreIntegrationAzureStorageAccountConnectionString)]
        public string ConnectionString
        {
            get;
            set;
        }





        /// <summary>
        /// Initializes a new instance of the <see cref="AzureStorageAccountDefaultConfigurationSettings"/> class.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AzureStorageAccountDefaultConfigurationSettings()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            ResourceNameSuffix = "";
        }



    }
}
