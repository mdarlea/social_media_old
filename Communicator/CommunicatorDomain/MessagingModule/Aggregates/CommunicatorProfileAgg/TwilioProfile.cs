using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.Seedwork.Extensions;

namespace Swaksoft.Domain.Communicator.MessagingModule.Aggregates.CommunicatorProfileAgg
{   
    /// <summary>
    /// Aggregate root for the Twilio configuration
    /// </summary>
    public class TwilioProfile : CommunicatorProfile
    {
        #region properties
        
        public TwilioAuthToken AuthorizationToken { get; set; }
        
        public int CallTimeoutSeconds { get; set; }

        /// <summary>
        /// The call timeout in seconds
        /// </summary>
        [NotMapped]
        public TimeSpan CallTimeout
        {
            get
            {
                return TimeSpan.FromSeconds(CallTimeoutSeconds);
            }
            set
            {
                CallTimeoutSeconds = value.Seconds;
            }
        }
        
        [Required]
        public Url TwimlUrlTemplate { get; set; }
       
        #endregion properties

        public string GetFormattedPhoneNumber(string phoneNumber)
        {
            var phone = GetPhoneNumber(phoneNumber);
            return (phone == null) ? null : string.Format("{0:twilio}", phone);
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return this.ValidationResults(base.Validate(validationContext))
                .NotNull(m => m.AuthorizationToken)
                .Execute();
        }

        public override Url CreateUrl(object routeValues)
        {
            return TwimlUrlTemplate.ToUrl(routeValues);
        }
    }
}
