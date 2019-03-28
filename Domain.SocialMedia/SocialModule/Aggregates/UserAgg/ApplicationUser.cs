using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Address = Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg.Address;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg
{
    public partial class User
    {
        [Required]
        public Name Name { get; set; }
        
        [MaxLength(100)]
        public string FacebookId { get; set; }

        [MaxLength(100)]
        public string SkypeId { get; set; }

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

        public bool AddAddress(Address address)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));

            //check if new address
            if (address.Id <= 0)
            {
                Addresses.Add(address);
                return true;
            }

            //check if user is already assciated with this address
            var addr = Addresses.FirstOrDefault(a => a.Id == address.Id);
            if (addr == null)
            {
                Addresses.Add(address);
                return true;
            }
            return false;
        }
    }
}