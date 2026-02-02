using App.Modules.Sys.Infrastructure.Constants;
using App.Modules.Sys.Shared.Attributes;
using App.Modules.Sys.Substrate.Models.ConfigurationSettings;

namespace App.Modules.Sys.Infrastructure.Models.Configuration.Implementations
{
    /// <summary>
    /// An immutable host configuration object 
    /// describing the configuration of 
    /// EF CodeFirst.
    /// </summary>
    public class CodeFirstMigrationConfigurationSettings : IHostSettingsBasedConfigurationObject
    {
        /// <summary>
        /// Attach the debugger to Code First
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSetting)]
        [Alias(ConfigurationKeys.AppCoreCodeFirstAttachDebuggerToPSSeeding)]
        public bool CodeFirstAttachDebugger { get; set; }

        /// <summary>
        /// Include Demo records when seeding.
        /// </summary>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSetting)]
        [Alias(ConfigurationKeys.AppCoreCodeFirstSeedIncludeDemoEntries)]
        public bool CodeFirstSeedDemoStuff { get; set; }

    }
}