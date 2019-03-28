using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.Seedwork.Extensions;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.EventAgg
{
    public class Event : Entity, IValidatableObject
    {
        #region properties
        [MaxLength(50)]
        [Required(ErrorMessage="The event name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        public int AddressId { get; private set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; private set; }
        
        [Required(ErrorMessage = "Start time is required")]
        public virtual DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        public virtual DateTime EndTime { get; set; }

        public bool? IsDeleted { get; private set; }

        [Required]
        public string UserId { get; private set; }

        public virtual User User { get; private set; }

        public bool? Repeat { get; set; }

		public string RecurrencePattern { get; set; }

		public string RecurrenceException { get; set; }

		#endregion properties

		public void DeleteEvent()
        {
            IsDeleted = true;
        }
        public void SetTheAddressForThisEvent(Address address)
        {
            if (address == null || address.IsTransient())
            {
                throw new ArgumentException("Cannot associate transient or null address");
            }
            
            AddressId = address.Id;
            Address = address;
        }
        
        public void SetTheAddressReference(int addressId)
        {
            if (addressId > 0)
            {
                //fix relation
                AddressId = addressId;
                Address = null;
            }
        }
        
        public void SetTheUserForThisEvent(User user)
        {
            if (user == null || user.IsTransient())
            {
                throw new ArgumentException("Cannot associate transient or null decription");
            }
            
            UserId = user.Id;
            User = user;
        }
        
        public void SetTheUserReference(string userId)
        {
            if (!string.IsNullOrWhiteSpace(userId))
            {
                UserId = userId;

                User = null;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return this.ValidationResults()
                    .Validate(a => a.StartTime, 
                        date => (IsDeleted==null || !IsDeleted.Value) && date.CompareTo(DateTime.Now) < 0, "Start time cannot be in the past!")
                    .Validate(a => a.EndTime, 
                        date => (IsDeleted == null || !IsDeleted.Value) && date.CompareTo(StartTime) < 0, 
                        "End time must be greater than start time!")
                   .Execute();
        }

       
    }
}
