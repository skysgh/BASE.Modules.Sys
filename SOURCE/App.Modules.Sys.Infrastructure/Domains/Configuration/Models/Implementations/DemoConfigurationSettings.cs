using App.Modules.Sys.Infrastructure.Domains.Constants;
using App.Modules.Sys.Shared.Attributes;

namespace App.Modules.Sys.Infrastructure.Domains.Configuration.Models.Implementations
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
