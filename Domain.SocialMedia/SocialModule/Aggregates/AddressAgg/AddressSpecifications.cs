using System;
using Swaksoft.Domain.Seedwork.Specification;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg
{
    public static class AddressSpecifications
    {
        public static ISpecification<Address> AddressLatitudeAndLongitude(double latitude, double longitude)
        {
            var latitudeSpecification = new DirectSpecification<Address>(a => a.Latitude.Equals(latitude));
            var longitudeSpecification = new DirectSpecification<Address>(a => a.Longitude.Equals(longitude));

            return latitudeSpecification && longitudeSpecification;
        }
    }
}
