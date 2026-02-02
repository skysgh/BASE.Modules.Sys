using App.Modules.Sys.Shared.Models.Entities.Base;
using App.Modules.Sys.Shared.Attributes;

namespace App.Modules.Sys.Shared.Models.Entities.Demos
{
    /// <summary>
    /// A demonstration of a Reference Type Entity
    /// used to describe an instance of
    /// <see cref="ExampleBEntity"/>
    /// </summary>
    [ForDemoOnly]
    public class ExampleBReferenceTypeEntity :
        UntenantedAuditedRecordStatedTimestampedGuidIdReferenceDataEntityBase
    {

    }
}
