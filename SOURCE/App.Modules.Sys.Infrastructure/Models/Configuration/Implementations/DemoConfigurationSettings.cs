using App.Modules.Sys.Infrastructure.Constants;
using App.Modules.Sys.Shared.Attributes;
using App.Modules.Sys.Substrate.Models.ConfigurationSettings;

namespace App.Modules.Sys.Infrastructure.Models.Configuration.Implementations
{
    /// <summary>
    /// TODO: Better documentation
    /// </summary>
    public class DemoConfigurationSettings : IHostSettingsBasedConfigurationObject
    {
        /// <summary>
        /// TODO: Better documentation
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSetting)]
        [Alias(ConfigurationKeys.AppCoreDemoMode)]
        public bool DemoMode
        {
            get; set;
        }
    }
}
