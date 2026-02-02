// namespace App.Modules.Sys.Substrate.Models.Entities
// {
//    using App.Modules.Sys.Substrate.Factories;

//    public class ReferenceDataBase : UntenantedTimestampedAuditedRecordStatedGuidIdEntityBase, IHasEnabled, IHasIsResourced,
//        IHasKeyValue<string>, IHasDisplayOrderHint
//    {
//        public ReferenceDataBase()
//        {
//            this.Id = GuidFactory.NewGuid();
//        }

//        public int DisplayOrderHint { get; set; }
//        public bool Enabled { get; set; }
//        public bool IsResourced { get; set; }
//        public string Key { get; set; }
//        public string Value { get; set; }
//    }
//}