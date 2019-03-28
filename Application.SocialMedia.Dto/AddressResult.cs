using Swaksoft.Core.Dto;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class AddressResult : ActionResult
    {
        public int Id { get; set; }
		public string PlaceName { get; set; }
		public string StreetAddress { get; set; }
        public string SuiteNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string CountryIsoCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int GeolocationStreetNumber { get; set; }
        public string GeolocationStreet { get; set; }
        public bool IsMainAddress { get; set; }
        public int? AddressTypeId { get; set; }
    }
}
