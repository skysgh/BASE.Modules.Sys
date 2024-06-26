﻿using App.Modules.Base.Shared.Models.TODO.Messages;
using App.Modules.Base.Substrate.Models.Messages;

namespace App.Modules.TmpSys.Application.APIs.TODO.Messages.V0100
{
    /// <summary>
    /// API DTO for system 
    /// <see cref="GeoInformationCountryRegion"/>
    /// </summary>
    public class GeoInformationCountryRegionDto
    {
        /// <summary>
        /// Iso Code of the Country Region.
        /// <para>
        /// TODO: Document explaination as to why this is such a small message.
        /// </para>
        /// </summary>
        public string? IsoCode { get; set; }
    }
}
