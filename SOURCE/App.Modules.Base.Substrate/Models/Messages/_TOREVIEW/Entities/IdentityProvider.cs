using App.Modules.Base.Substrate.Factories;
using App.Modules.Base.Substrate.Models.Contracts;
using App.Modules.Base.Substrate.Models.Contracts.Enums;

namespace App.Modules.Base.Substrate.Models.Messages._TOREVIEW.Entities
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
        public byte[] Timestamp { get; set; }
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

        public string ProviderKey { get; set; }
        /// <summary>
        /// TODO: Describe better
        /// </summary>
        public string UserId { get; set; }
    }
}
