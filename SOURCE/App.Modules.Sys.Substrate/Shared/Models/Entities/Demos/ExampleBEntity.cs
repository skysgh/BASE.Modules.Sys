using App.Modules.Sys.Shared.Models.Entities.Base;
using App.Modules.Sys.Shared.Attributes;

namespace App.Modules.Sys.Shared.Models.Entities.Demos
{
    /// <summary>
    /// A slighly more complex example entity,
    /// with a single navigable property
    /// </summary>
    [ForDemoOnly]
    public class ExampleBEntity :
        UntenantedAuditedRecordStatedTimestampedGuidIdEntityBase
    {

        /// <summary>
        /// A Demo Title
        /// </summary>
        public string Title { get; set; } = default!;

        /// <summary>
        /// A Demo Description
        /// </summary>
        public string Description { get; set; } = string.Empty;


        /// <summary>
        /// A single Property FK
        /// </summary>
        public Guid SinglePropertyFK { get; set; }
        /// <summary>
        /// A single Property
        /// </summary>
        public ExampleBReferenceTypeEntity SingleProperty { get; set; } = default!;

    }
}
