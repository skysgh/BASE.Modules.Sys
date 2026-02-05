namespace App.Modules.Core.Infrastructure.Models.Configuration._TOREVIEW
{
    using App.Base.Shared.Attributes;

    /// <summary>
    /// AppHost/KeyVault sourced settings required to configure
    /// the DocumentDbService.
    /// </summary>
    /// <seealso cref="IHostSettingsBasedConfigurationObject" />
    public class AzureDocumentDbConfigurationSettings : IHostSettingsBasedConfigurationObject, IKeyVaultBasedConfigurationObject
    {
        /// <summary>
        /// Gets or sets the root name for Azure resource names.
        /// <para>
        /// If not provided in AppSettings, using
        /// <see cref="Shared.Constants.ConfigurationKeys.AppCoreIntegrationAzureDocumentDbResourceName"/>
        /// falls back to 
        /// <see cref="Shared.Constants.ConfigurationKeys.AppCoreIntegrationAzureCommonResourceName"/>
        /// </para>
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSetting)]
        [Alias(Constants.ConfigurationKeys.AppCoreIntegrationAzureDocumentDbResourceName)]
        public string? ResourceName { get; set; }


        //[Alias(Constants.ConfigurationKeys.AppCoreIntegrationAzureDocumentDbEndpointUrl)]
        //public string EndpointUrl { get; set; }

        /// <summary>
        /// Gets or sets the authorization key.
        /// <para>
        /// Note: should not be needed if we are using MSI
        /// </para>
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.KeyVault)]
        [Alias(Constants.ConfigurationKeys.AppCoreIntegrationAzureDocumentDbAuthorizationKey)]
        public string? AuthorizationKey { get; set; }



    }
}
