using App.Modules.Sys.Shared.Factories;
using App.Modules.Sys.Shared.Models.Enums;
using App.Modules.Sys.Shared.Models.Persistence;
using App.Modules.Sys.Shared.Attributes;

namespace App.Modules.Sys.Shared.Models.Entities.Demos
{
    /// <summary>
    /// Internal Simple (no Children) Demo Entity
    /// <para>
    /// Used to demonstrate correct mapping
    /// from an internal entity to an external
    /// entity, using an ObjectMapping 
    /// implementing
    /// <c></c>
    /// </para>
    /// </summary>
    [ForDemoOnly]
    public class ExampleAEntity :
        IHasGuidId,
        IHasRecordState,
        IHasEnabled,
        IHasTitle,
        IHasDescription
    {
        /// <inheritdoc/>
        public Guid Id { get; set; } = GuidFactory.NewGuid();

        /// <inheritdoc/>
        public RecordPersistenceState RecordState { get; set; }

        /// <inheritdoc/>
        public bool Enabled { get; set; }

        /// <inheritdoc/>
        public string Title { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string Description { get; set; } = string.Empty;
    }
}
