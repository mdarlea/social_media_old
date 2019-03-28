using System.Collections.Generic;
using System.Threading.Tasks;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<IList<Address>> FindAllInArea(double longitude, double latitude, int meters);
    }
}
