using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Swaksoft.Domain.Seedwork.Aggregates.ProfileAgg;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.Seedwork.Extensions;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorPhoneNumberAgg;
using Swaksoft.Domain.Communicator.Resources;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg
{   
    /// <summary>
    /// Aggregate root for providers configuration
    /// </summary>
    public abstract class CommunicatorProfile : Profile, IValidatableObject, IUrlCreator
    {
        #region properties

        public int DefaultPhoneNumberId { get; set; }
        public CommunicatorPhoneNumber DefaultPhoneNumber { get; set; }
        
        //lazy loading
        private ICollection<CommunicatorPhoneNumber> _allowNumbers;
        public ICollection<CommunicatorPhoneNumber> AllowNumbers
        {
            get
            {
                return _allowNumbers ?? (_allowNumbers = new HashSet<CommunicatorPhoneNumber>());
            }
            set
            {
                _allowNumbers = value;
            }
        }
        #endregion properties

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return this.ValidationResults()
                .Execute();
        }

        #region methods
        public string GetFormattedPhoneNumber(string phoneNumber, string format)
        {
            var phone = GetPhoneNumber(phoneNumber);
            return (phone == null) ? null : string.Format(format, phone);
        }

        public CommunicatorPhoneNumber AddNewPhoneNumber(PhoneNumber phoneNumber)
        {
            var newPhoneNumber = new CommunicatorPhoneNumber
            {
                ProfileId = Id,
                Profile = this,
                PhoneNumber = phoneNumber
            };
            AllowNumbers.Add(newPhoneNumber);
            return newPhoneNumber;
        }

        public CommunicatorPhoneNumber GetPhoneNumber(string phoneNumber)
        {
            //validates the phone number
            PhoneNumber phone;
            if (!PhoneNumber.TryParse(phoneNumber, out phone))
            {
                throw new ArgumentException(Messages.validation_InvalidPhoneNumber, "phoneNumber");
            }

            return AllowNumbers.FirstOrDefault(p => p.PhoneNumber.Equals(phone));
        }

        public bool IsValidPhoneNumber(PhoneNumber phoneNumber)
        {
            return AllowNumbers.Any(p => p.PhoneNumber.Equals(phoneNumber));
        }
        #endregion methods

        public abstract Url CreateUrl(object routeValues);
    }
}
