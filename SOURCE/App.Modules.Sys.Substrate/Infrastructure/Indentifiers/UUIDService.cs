using App.Modules.Sys.Infrastructure.Domains.Identifiers;
using App.Modules.Sys.Shared.Factories;

namespace App.Modules.Sys.Infrastructure.Indentifiers
{
    ///<inheritdoc/>
    public class UUIDService : IUUIDService
    {
        ///<inheritdoc/>
        public Guid Generate()
        {
            return GuidFactory.NewGuid();
        }
    }
}
