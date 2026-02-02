using System;
using System.Collections.Generic;
using System.Text;

namespace App.Modules.Sys.Domain.Services
{
    /// <summary>
    /// Contract that Domain Services 
    /// of this logical module must implement
    /// to be discoverable by reflection at startup. 
    /// </summary>
    public interface IHasAppDomainService: IHasDomainService
    {
    }
}
