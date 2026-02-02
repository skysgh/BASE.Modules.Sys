// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
using App.Modules.Sys.Shared.ExtensionMethods;
using App.Modules.Sys.Shared.Models.Entities.Demos;
using App.Modules.Sys.Shared.Models.Enums;
using App.Modules.Sys.Shared.Attributes;
// using App.Modules.Sys.Substrate.Attributes;

namespace App.Modules.Sys.Shared.Factories.Demo
{
    /// <summary>
    /// Static Factory to develop simple
    /// <see cref="ExampleAEntity"/> entities
    /// that have just scalar properties, and no
    /// child records or collections of records
    /// </summary>
    [ForDemoOnly]
    internal static partial class ExampleAEntityFactory
    {

        /// <summary>
        /// Static method to build a
        /// <see cref="ExampleAEntity"/>
        /// and return it.
        /// </summary>
        /// <param name="index"></param>
        public static ExampleAEntity Build(int index)
        {
            ExampleAEntity result = new()
            {
                RecordState = RecordPersistenceState.Active,
                Id = index.ToGuid(),
                Title = "Some Displayable Text",
                Description = "Some Description of the item...blah, blah...",
            };
            return result;
        }
    }
}
