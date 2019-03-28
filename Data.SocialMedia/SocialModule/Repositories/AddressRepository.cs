using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg;
using Swaksoft.Infrastructure.Data.Seedwork.Repositories;

namespace Swaksoft.Infrastructure.Data.SocialMedia.SocialModule.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(ITransactionUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public async Task<IList<Address>> FindAllInArea(double longitude, double latitude, int meters)
        {
            return await Context.Database.SqlQuery<Address>("CALL AddressesInAdrea({0}, {1}, {2})", longitude, latitude, meters).ToListAsync();
        }
    }
}
