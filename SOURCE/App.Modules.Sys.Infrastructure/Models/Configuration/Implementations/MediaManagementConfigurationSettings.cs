// using App.Modules.Sys.Substrate.Models.Configuration;
using App.Modules.Sys.Infrastructure.Constants;
using App.Modules.Sys.Shared.Attributes;
using App.Modules.Sys.Substrate.Models.ConfigurationSettings;

namespace App.Modules.Sys.Infrastructure.Models.Configuration.Implementations
{
    /// <summary>
    /// A Configuration settings package to use when configuring Media Management
    /// </summary>
    public class MediaManagementConfigurationSettings : IHostSettingsBasedConfigurationObject
    {
        private string? _hashType;

        /// <summary>
        /// THe Hash type to use when making the hash
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSetting)]
        [Alias(ConfigurationKeys.AppCoreMediaHashType)]
        public string HashType
        {
            get => this._hashType ?? "SHA-256";
            set => this._hashType = value;
        }
    }
}