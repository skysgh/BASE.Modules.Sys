
namespace App.Modules.Base.Substrate.Models.Messages
{
    /// <summary>
    /// TODO: Describe
    /// </summary>
    public class GeoInformationCountryRegion
    {
        /// <summary>
        /// The ISO code of the country.
        /// </summary>
        public string IsoCode { get => _isoCode; set => _isoCode = value ?? String.Empty; }
        private string _isoCode = String.Empty;
    }
}
