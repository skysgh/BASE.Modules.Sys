// namespace App.Modules.Sys.Substrate.Models.Configuration.AppHost
// {
//     using App.Modules.Sys.Substrate.Attributes;
//     using App.Modules.Sys.Substrate.Models.ConfigurationSettings;

//     public class Service02Configuration: IHostSettingsBasedConfigurationObject
//     {

//         // Make sure this kind of secrets are not gotten from AppSettings.
//         [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.KeyVault)]
//         [Alias(Constants.ConfigurationKeys.AppCoreIntegrationService02ClientId)]
//         public string Key
//         {
//             get; set;
//         }

//         // Make sure this kind of secrets are not gotten from AppSettings.
//         [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.KeyVault)]
//         [Alias(Constants.ConfigurationKeys.AppCoreIntegrationService02ClientSecret)]
//         public string Secret
//         {
//             get; set;
//         }


//         [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSettingsViaDeploymentPipeline)]
//         [Alias(Constants.ConfigurationKeys.AppCoreIntegrationService02BaseUri)]
//         public string BaseUri
//         {
//             get; set;
//         }


//         [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSettingsViaDeploymentPipeline)]
//         [Alias(Constants.ConfigurationKeys.AppCoreIntegrationService02MiscConfig)]
//         public string MiscConfig
//         {
//             get; set;
//         }

//     }
// }