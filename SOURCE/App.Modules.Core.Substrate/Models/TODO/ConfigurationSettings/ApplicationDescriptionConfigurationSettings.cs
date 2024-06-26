﻿namespace App.Modules.TmpSys.Shared.Models.TODO.ConfigurationSettings
{
    using System;
    using App.Modules.TmpSys.Substrate.tmp.Attributes;
    using App.Modules.TmpSys.Substrate.tmp.Constants;
    using App.Modules.TmpSys.Substrate.Models.Contracts;

    /// <summary>
    /// An immutable host configuration object 
    /// describing the Application (ie, shows up on the header).
    /// </summary>
    public class ApplicationDescriptionConfigurationSettings : IHostSettingsBasedConfigurationObject, IHasName, IHasDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDescriptionConfigurationSettings"/> class.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ApplicationDescriptionConfigurationSettings()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Id = new Guid();
        }


        /// <summary>
        /// The Id.
        /// <para>
        /// OData always needs an Id. It can be another field, but too much bother
        /// to configure it...
        /// </para>
        /// </summary>
        public Guid Id
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the name of the Application
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSetting)]
        [Alias(ConfigurationKeys.AppCoreApplicationName)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description/byline of the Application.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [ConfigurationSettingSource(ConfigurationSettingSource.SourceType.AppSetting)]
        [Alias(ConfigurationKeys.AppCoreApplicationDescription)]
        public string Description { get; set; }
    }
}
