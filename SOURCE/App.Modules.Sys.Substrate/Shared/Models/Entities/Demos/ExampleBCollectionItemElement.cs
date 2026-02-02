using App.Modules.Sys.Shared.Attributes;
using App.Modules.Sys.Shared.Models.Entities.Base;
using App.Modules.Sys.Shared.Models.Persistence;

namespace App.Modules.Sys.Shared.Models.Entities.Demos
{
    /// <summary>
    /// Example of an Item in a list that belongs
    /// to a parent object
    /// </summary>
    [ForDemoOnly]
    public class ExampleBCollectionItemElement
        : UntenantedAuditedRecordStatedTimestampedGuidIdEntityBase,
        IHasTitleAndDescription,
        IHasGenericValue<int>,
        IHasOwnerFK

    {
        /// <summary>
        /// Gets the FK of the owner of this 
        /// collection item.
        /// </summary>
        public Guid OwnerFK { get; set; }

        /// <inheritdoc/>
        public string Title { get; set; } = string.Empty;
        /// <inheritdoc/>
        public string Description { get; set; } = string.Empty;
        /// <inheritdoc/>
        public int Value { get; set; }

        /// <inheritdoc/>
        public Guid GetOwnerFk()
        {
            return OwnerFK;
        }
    }
}
