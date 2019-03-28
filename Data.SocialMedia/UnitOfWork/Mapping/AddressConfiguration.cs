using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg;

namespace Swaksoft.Infrastructure.Data.SocialMedia.UnitOfWork.Mapping
{
    public class AddressConfiguration : EntityTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            HasOptional(a => a.AddressType).WithMany(at => at.Addresses).HasForeignKey(a => a.AddressTypeId);
        }
    }
}
