using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.EventAgg
{
    public static class EventFactory
    {
        public static Event CreateEvent(string name,
            string descrription,
            DateTime startTime,
            DateTime endTime,
            Address address,
            User user,
            string recurrencePattern,
			string recurrenceException)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));
            if (user == null) throw new ArgumentNullException(nameof(user));

            //creates a new event
            var newEvent = new Event()
            {
                Name = name,
                Description = descrription,
                StartTime = startTime,
                EndTime = endTime,
                RecurrencePattern = recurrencePattern,
				RecurrenceException = recurrenceException
            };
			if (!string.IsNullOrWhiteSpace(recurrencePattern)) {
				newEvent.Repeat = true;
			}
            newEvent.SetTheAddressForThisEvent(address);
            newEvent.SetTheUserForThisEvent(user);

            return newEvent;
        }
    }
}
