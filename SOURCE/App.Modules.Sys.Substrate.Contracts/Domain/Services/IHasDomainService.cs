using App.Modules.Sys.Shared.Services;

namespace App.Modules.Sys.Domain.Services
{
    /// <summary>
    /// Contract that needs to be implemented by 
    /// Domain Services to be discoverable by
    /// reflection at startup.
    /// </summary>
    public interface IHasDomainService: IHasService
    {
    }
}
