using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressTypeAgg
{
    public class AddressType :Entity
    {
        private AddressType() {}

        public AddressType(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            Name = name;
        }

        public string Name { get; set; }

        private HashSet<Address> addresses;
        public virtual ICollection<Address> Addresses
        {
            get
            {
                return addresses ?? (addresses = new HashSet<Address>());
            }
            set
            {
                addresses = new HashSet<Address>(value);
            }
        }
    }
}
