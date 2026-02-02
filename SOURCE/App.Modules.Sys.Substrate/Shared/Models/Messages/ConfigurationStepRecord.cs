using App.Modules.Sys.Shared.Factories;
using App.Modules.Sys.Shared.Models.Messages.Enums;
using App.Modules.Sys.Shared.Models.Persistence;

namespace App.Modules.Sys.Shared.Models.Messages
{
    /// <summary>
    /// A record of a configuration step that was undertaken.
    /// For use by support personnel remotely reviewing configuration.
    /// </summary>
    public class ConfigurationStepRecord : IHasGuidId, IHasTitleAndDescription
    {


        /// <summary>
        /// <para>
        /// Note than although this model is not persisted in 
        /// a datastore, an Id is still required, as it is expressed
        /// via OData.
        /// </para>
        /// </summary>
        public ConfigurationStepRecord()
        {
            Id = GuidFactory.NewGuid();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dateTime"></param>
        public ConfigurationStepRecord(DateTimeOffset dateTime)
        {
            DateTime = dateTime;
        }

        /// <summary>
        /// The Record Identifier
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The <see cref="ConfigurationStepType"/>
        /// as to whether about setting up Security, Peformance, etc.
        /// </summary>
        public ConfigurationStepType Type { get; set; } = ConfigurationStepType.Undefined;

        /// <summary>
        /// The <see cref="ConfigurationStepStatus"/>
        /// as to whether it was successful or not.
        /// </summary>
        public ConfigurationStepStatus Status { get; set; } = ConfigurationStepStatus.Undefined;

        /// <summary>
        /// The <see cref="DateTimeOffset"/> 
        /// at which the event occurred.
        /// </summary>
        public DateTimeOffset DateTime { get; set; } = DateTimeOffset.MinValue;
        /// <summary>
        /// The display Title of the configuration step event.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The display Description of the configuration step event.
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
