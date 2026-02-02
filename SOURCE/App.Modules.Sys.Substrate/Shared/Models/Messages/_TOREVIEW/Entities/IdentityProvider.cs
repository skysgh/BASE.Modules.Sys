using App.Modules.Sys.Shared.Factories;
using App.Modules.Sys.Shared.Models.Enums;
using App.Modules.Sys.Shared.Models.Persistence;

namespace App.Modules.Sys.Shared.Models.Messages._TOREVIEW.Entities
{
    /// <summary>
    /// TODO: Describe better
    /// </summary>
    public class IdentityProvider : IHasGuidId, IHasTimestamp, IHasRecordState, IHasKey
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public IdentityProvider()
        {
            GuidFactory.NewGuid();
        }

        /// <summary>
        /// TODO: Describe better
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// TODO: Describe better
        /// </summary>
        public byte[] Timestamp { get; set; } = null!;
        /// <summary>
        /// TODO: Describe better
        /// </summary>
        public RecordPersistenceState RecordState { get; set; }

        /// <summary>
        /// TODO: Describe better
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// TODO: Describe better
        /// </summary>

        public string ProviderKey { get; set; } = default!;

        /// <summary>
        /// TODO: Describe better
        /// </summary>
        public string UserId { get; set; } = default!;
    }
}
