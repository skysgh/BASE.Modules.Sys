// using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace App.Modules.Sys.Infrastructure.Services.Azure.Domains.Mapping.Models.Implementations
{

    /// <summary>
    /// Azure Maps Search response
    /// </summary>
    public class AzureMapsSearchResponse
    {
        /// <summary>
        /// The array of <see cref="AzureMapsSearchResponseResult"/>
        /// </summary>
        [JsonPropertyName("results")]
        public AzureMapsSearchResponseResult[] Results { get; set; } = [];
    }

    /// <summary>
    /// A single Azure Maps response, 
    /// <para>
    /// Generally within an array, within
    /// <see cref="AzureMapsSearchResponse"/>
    /// </para>
    /// </summary>
    public class AzureMapsReverseSearchResponse
    {
        /// <summary>
        /// a list of <see cref="AzureMapsResponseAddress"/>.
        /// </summary>
        [JsonPropertyName("addresses")]
        public List<AzureMapsResponseAddress> Addresses => _addresses ??= []; //new List<AzureMapsResponseAddress>()

        private List<AzureMapsResponseAddress>? _addresses;
    }


    /// <summary>
    /// A single Azure Maps Search Response
    /// </summary>
    public class AzureMapsSearchResponseResult
    {
        private AzureMapsResponseAddress? _address;
        private AzureMapResponsePosition? _position;

        /// <summary>
        /// The Type of response.
        /// </summary>
        [JsonPropertyName("results")]
        public string? Type { get; set; }


        /// <summary>
        /// The response score.
        /// </summary>
        [JsonPropertyName("score")]
        public string? Score { get; set; } 

        /// <summary>
        /// The Response <see cref="AzureMapsResponseAddress"/>
        /// </summary>
        [JsonPropertyName("address")]
        public AzureMapsResponseAddress Address { get => _address ??= new(); set => _address = value; }

        /// <summary>
        /// The response <see cref="AzureMapResponsePosition"/>
        /// </summary>
        [JsonPropertyName("position")]
        public AzureMapResponsePosition Position { get => _position ??= new(); set => _position = value; }
    }

    /// <summary>
    /// Azure Maps response for Position.
    /// </summary>
    public class AzureMapResponsePosition
    {

        /// <summary>
        /// Latitude
        /// </summary>
        [JsonPropertyName("lat")]
        public string? Latitude { get; set; } 

        /// <summary>
        /// Longitude
        /// </summary>
        [JsonPropertyName("lon")]
        public string? Longitude { get; set; } 
    }


    /// <summary>
    /// Response from Azure Maps.
    /// </summary>
    public class AzureMapsResponseAddress
    {
        private string? freeFormAddress = String.Empty;
        private string? postalCodeExtended = String.Empty;
        private string? postalCode = String.Empty;
        private string? countryCode = String.Empty;
        private string? countryCodeISO3 = String.Empty;
        private string? buildingNumber = String.Empty;
        private string? streetNumber = String.Empty;
        private string? street = String.Empty;
        private string? streetName = String.Empty;
        private string? streetNameAndNumber = String.Empty;
        private string? municipalitySubDivision = String.Empty;
        private string? municipality = String.Empty;
        private string? countrySecondarySubdivision;
        private string? countrySubdivision = String.Empty;
        private string? country = String.Empty;

        /// <summary>
        /// Address Building Number
        /// </summary>
        [JsonPropertyName("buildingNumber")]
        public string BuildingNumber { get => buildingNumber ??= String.Empty; set => buildingNumber = value; }
        /// <summary>
        /// Address Street number
        /// </summary>
        [JsonPropertyName("streetNumber")]
        public string StreetNumber { get => streetNumber ??= String.Empty; set => streetNumber = value; }

        /// <summary>
        /// Address Street
        /// </summary>
        [JsonPropertyName("street")]
        public string Street { get => street ?? String.Empty; set => street = value; }
        /// <summary>
        /// Address Street name
        /// </summary>
        [JsonPropertyName("streetName")]
        public string StreetName { get => streetName ?? String.Empty; set => streetName = value; }
        /// <summary>
        /// Address Street name and Number
        /// </summary>
        [JsonPropertyName("streetNameAndNumber")]
        public string StreetNameAndNumber { get => streetNameAndNumber ??= String.Empty; set => streetNameAndNumber = value; }
        /// <summary>
        /// Address Municipality Subdivision
        /// </summary>
        [JsonPropertyName("municipalitySubDivision")]
        public string MunicipalitySubDivision { get => municipalitySubDivision ?? String.Empty; set => municipalitySubDivision = value; }
        /// <summary>
        /// Address Municipality
        /// </summary>
        [JsonPropertyName("municipality")]
        public string Municipality { get => municipality ?? String.Empty; set => municipality = value; }

        /// <summary>
        /// Address Street number
        /// </summary>
        [JsonPropertyName("countrySecondarySubdivision")]
        public string CountrySecondarySubdivision { get => countrySecondarySubdivision ?? String.Empty; set => countrySecondarySubdivision = value; }

        /// <summary>
        /// Address Country Subdivision
        /// </summary>
        [JsonPropertyName("countrySubdivision")]
        public string CountrySubdivision { get => countrySubdivision ?? String.Empty; set => countrySubdivision = value; }

        /// <summary>
        /// Address Country Code ISO3
        /// </summary>
        [JsonPropertyName("countryCodeISO3")]
        public string CountryCodeISO3 { get => countryCodeISO3 ?? String.Empty; set => countryCodeISO3 = value; }
        /// <summary>
        /// Address Country Code
        /// </summary>
        [JsonPropertyName("countryCode")]
        public string CountryCode { get => countryCode ?? String.Empty; set => countryCode = value; }
        /// <summary>
        /// Address Country
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get => country ?? String.Empty; set => country = value; }
        /// <summary>
        /// Address Postal Code
        /// </summary>
        [JsonPropertyName("postalCode")]
        public string PostalCode { get => postalCode ?? String.Empty; set => postalCode = value; }
        /// <summary>
        /// Address Extended Postal Code
        /// </summary>
        [JsonPropertyName("extendedPostalCode")]
        public string PostalCodeExtended { get => postalCodeExtended ?? String.Empty; set => postalCodeExtended = value; }

        /// <summary>
        /// Address Free form
        /// </summary>
        [JsonPropertyName("freeformAddress")]
        public string FreeFormAddress { get => freeFormAddress ?? String.Empty; set => freeFormAddress = value; }

        /// <summary>
        /// Address Extended Free Form Address
        /// </summary>
        [JsonPropertyName("freeformAddressExtended")]
        public string ExtendedFreeFormAddress => this.FreeFormAddress + ", " + this.Country;

        /// <summary>
        /// Address Route numbers
        /// </summary>
        [JsonPropertyName("routeNumbers")]
        public string[] RouteNumbers { get; set; } = [];
    }


}

