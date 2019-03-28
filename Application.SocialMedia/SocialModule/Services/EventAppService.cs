using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swaksoft.Application.Seedwork.Extensions;
using Swaksoft.Application.Seedwork.Services;
using Swaksoft.Application.SocialMedia.Dto;
using Swaksoft.Core.Dto;
using Swaksoft.Domain.Seedwork.Specification;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.EventAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Services;
using Swaksoft.Infrastructure.Crosscutting.Extensions;
using Swaksoft.Infrastructure.Crosscutting.Validation;
using Address = Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg.Address;
using Event = Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.EventAgg.Event;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public class EventAppService :AppServiceBase<EventAppService>, IEventAppService
    {
        private readonly IEventRepository eventRepository;
        private readonly IUserRepository userRepository;
        private readonly IAddressRepository addressRepository;
        private readonly IAddressService addressService;

        public EventAppService(
            IEventRepository eventRepository, 
            IUserRepository userRepository,
            IAddressRepository addressRepository,
            IAddressService addressService)
        {
            if (eventRepository == null) throw new ArgumentNullException(nameof(eventRepository));
            if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
            if (addressRepository == null) throw new ArgumentNullException(nameof(addressRepository));
            if (addressService == null) throw new ArgumentNullException(nameof(addressService));
            this.eventRepository = eventRepository;
            this.userRepository = userRepository;
            this.addressRepository = addressRepository;
            this.addressService = addressService;
        }
        
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing) return;

            eventRepository.Dispose();
            addressRepository.Dispose();
            userRepository.Dispose();
            addressService.Dispose();
        }

        public async Task<Dto.EventWithAddressResult> AddNewEventAsync(Dto.EventWithAddress @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));
            if(@event.AddressId < 1 && @event.Address==null) 
                throw new ArgumentException(@"The address must be provided","@event");

            var log = GetLog();

            //get the user associated with the event
            var user = userRepository.GetFiltered(u => u.Id == @event.UserId).SingleOrDefault();

            if (user == null)
            {
                throw new InvalidDataException($"Could not find a user with {@event.UserId} id");
            }

            //get the address associated  with this event
            Address address=null;
            if (@event.AddressId > 0)
            {
                address = addressRepository.Get(@event.AddressId);
            }
            else
            {
                var addressDto = @event.Address;
               
                address = await addressService.GetAddressAsync(
                    addressDto.StreetAddress,
                    addressDto.SuiteNumber,
                    addressDto.City,
                    addressDto.State,
                    addressDto.Zip,
                    addressDto.Latitude,
                    addressDto.Longitude,
                    addressDto.GeolocationStreetNumber,
                    addressDto.GeolocationStreet,
                    addressDto.CountryIsoCode,
                    addressDto.IsMainAddress);
            }

            if (address.IsTransient())
            {
                var result = await addressRepository.SaveEntityAsync(address);
                if (!result.IsValid)
                {
                    return result.ProjectAs<Dto.EventWithAddressResult>();
                }
            }

            //creates the event
            var newEvent = EventFactory.CreateEvent(
                @event.Name,
                @event.Description,
                @event.StartTime,
                @event.EndTime,
                address,
                user,
                @event.RecurrencePattern,
				@event.RecurrenceException);
           
            //saves the  event
            var eventResult = await eventRepository.SaveEntityAsync(newEvent);

            return eventResult.ProjectAs<Dto.EventWithAddressResult>();
        }

        public async Task<EventWithAddressResult> UpdateEventWithAddress(EventWithAddress @event)
        {
            //check if address was changed
            var persistedAddress = (@event.AddressId > 0) ? addressRepository.Get(@event.AddressId) : null;

            if (persistedAddress == null) return await UpdateEventRequest(@event);

            var newAddress = AddressAppService.MaterializeAddressFromDto(@event.Address);
            if (persistedAddress.IsEqualWith(newAddress))
            {
                return await UpdateEventRequest<EventWithAddressResult>(new Dto.Event()
                {
                    AddressId = @event.AddressId,
                    Description = @event.Description,
                    EndTime = @event.EndTime,
                    Id = @event.Id,
                    Instructor = @event.Instructor,
                    Name = @event.Name,
                    StartTime = @event.StartTime,
                    UserId = @event.UserId,
					RecurrenceException = @event.RecurrenceException,
					RecurrencePattern = @event.RecurrencePattern
                }, true);
            }
            return await UpdateEventRequest(@event);
        }

        private async Task<EventWithAddressResult> UpdateEventRequest(EventWithAddress @event)
        {
            //create new address if it does not exist
            var address = @event.Address;

            var addr = await addressService.GetAddressAsync(
                address.StreetAddress,
                address.SuiteNumber,
                address.City,
                address.State,
                address.Zip,
                address.Latitude,
                address.Longitude,
                address.GeolocationStreetNumber,
                address.GeolocationStreet,
                address.CountryIsoCode,
                address.IsMainAddress);

            int addressId;
            if (addr.IsTransient())
            {
                var result = await addressRepository.SaveEntityAsync(addr);
				if (!result.IsValid) return result.ProjectAs<EventWithAddressResult>();

                addressId = result.Entity.Id;
            }
            else
            {
                addressId = addr.Id;
            }

            return await UpdateEventRequest<EventWithAddressResult>(new Dto.Event()
            {
                AddressId = addressId,
                Description = @event.Description,
                EndTime = @event.EndTime,
                Id = @event.Id,
                Instructor = @event.Instructor,
                Name = @event.Name,
                StartTime = @event.StartTime,
                UserId = @event.UserId
            }, true);
        }

		private async Task<T> UpdateEventRequest<T>(Dto.Event @event, bool getEventWithAddress) where T: Dto.EventResult, new()
		{
			if (@event == null) throw new ArgumentNullException(nameof(@event));

			//get the current address
			var persisted = eventRepository.Get(@event.Id);
			if (persisted == null)
			{
				throw new DataException($"Could not find an event with the {@event.Id} id");
			}

			//the updated address
			var current = MaterializeEventFromDto(@event);

			//validates the current event
			var entityValidator = EntityValidatorLocator.CreateValidator();
			var isValid = entityValidator.IsValid(current);
			if (!isValid)
			{
				return new T
				{
					Status = ActionResultCode.Failed,
					Message = "Invalid address",
					Errors = entityValidator.GetInvalidMessages(current).ToList()
				};
			}

			using (var uofw = eventRepository.BeginTransaction())
			{
				eventRepository.Merge(persisted, current);
				uofw.Commit();
			}

			if (getEventWithAddress)
			{
				var entity = await eventRepository.GetEventWithAddressAndUser(@event.Id);
				return entity.ProjectedAs<T>();
			}			
			return persisted.ProjectedAs<T>();
		}

        public async Task<EventResult> UpdateEvent(Dto.Event @event)
        {
			return await UpdateEventRequest<EventResult>(@event, false);
        }

        public void RemoveEvent(int eventId)
        {
            var @event = eventRepository.Get(eventId);
            using (var uofw = eventRepository.BeginTransaction())
            {
                @event.DeleteEvent();
                uofw.Commit();
            }
        }

        public async Task<Dto.EventWithAddressResult> FindEvent(int eventId)
        {
           if(eventId <0) throw new ArgumentException("Event id must be greater than 0", nameof(eventId));
            var result = await eventRepository.GetEventWithAddressAndUser(eventId);
            return result.ProjectedAs<Dto.EventWithAddressResult>();
        }

        public async Task<Dto.CollectionActionResult<Dto.Event>> FindDailyEventsForUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));

            var specification = EventSpecifications.DailyEventsForUser(userId);
            return await FindEvents(specification);
        }

        public async Task<Dto.CollectionActionResult<Dto.Event>> FindWeeklyEventsForUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));

            var specification = EventSpecifications.WeeklyEventsForUser(userId);
            return await FindEvents(specification);
        }

        public async Task<Dto.CollectionActionResult<Dto.Event>> FindMonthlyEventsForUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));

            var specification = EventSpecifications.MonthlyEventsForUser(userId);
            return await FindEvents(specification);
        }

        public async Task<Dto.CollectionActionResult<Dto.Event>> FindDailyEvents()
        {
            var specification = EventSpecifications.DailyEvents();
            return await FindEvents(specification);
        }

        public async Task<Dto.CollectionActionResult<Dto.Event>> FindWeeklyEvents()
        {
            var specification = EventSpecifications.WeeklyEvents();
            return await FindEvents(specification);
        }

        private async Task<Dto.CollectionActionResult<Dto.Event>> FindEvents(ISpecification<Event> specification)
        {
            var events = await eventRepository.GetEventsWithUserMatching(specification);

            return new Dto.CollectionActionResult<Dto.Event>()
            {
                Status = ActionResultCode.Success,
                Items = events.ProjectedAsCollection<Dto.Event>()
            };
        }

        public async Task<Dto.CollectionActionResult<Dto.Event>> FindMonthlyEvents()
        {
           var specification = EventSpecifications.MonthlyEvents();
           return await FindEvents(specification);
        }

        public async Task<CollectionActionResult<Dto.Event>> FindEventsInTimeRange(TimeRange timeRange)
        {
            if (timeRange == null) throw new ArgumentNullException(nameof(timeRange));

			var log = GetLog();			

			var specification = EventSpecifications.EventsInTimeRange(timeRange.StartTime, timeRange.EndTime);
            var result = await FindEvents(specification);			

			return result;
        }

        public async Task<CollectionActionResult<EventWithAddress>> FindEventsInArea(Location location)
        {
            var events = await eventRepository.FindAllInArea(location.Longitude, location.Latitude, location.Distance);
            return new Dto.CollectionActionResult<Dto.EventWithAddress>()
            {
                Status = ActionResultCode.Success,
                Items = events.ProjectedAsCollection<Dto.EventWithAddress>()
            };
        }


        private Event MaterializeEventFromDto(Dto.Event @event)
        {
            var address = new Address();
            address.ChangeCurrentIdentity(@event.AddressId);

            var user = new User();
            user.ChangeCurrentIdentity(@event.UserId);

            //creates new event
            var newEvent = EventFactory.CreateEvent(
                @event.Name,
                @event.Description,
                @event.StartTime,
                @event.EndTime,
                address,
                user,
                @event.RecurrencePattern,
				@event.RecurrenceException);
            newEvent.SetTheAddressReference(@event.AddressId);
            newEvent.SetTheUserReference(@event.UserId);

            newEvent.ChangeCurrentIdentity(@event.Id);

            return newEvent;
        }

        public async Task<CollectionActionResult<Dto.Event>> FindEventsInTimeRangeForUser(Dto.EventForUserRequest eventForUser)
        {
            if (eventForUser == null) throw new ArgumentNullException(nameof(eventForUser));
            if (eventForUser.TimeRange == null) throw new ArgumentNullException(nameof(eventForUser));
			
			var specification = EventSpecifications.EventsInTimeRangeForUser(
                eventForUser.UserId,
				eventForUser.TimeRange.StartTime,
				eventForUser.TimeRange.EndTime);
            return await FindEvents(specification);
        }
    }
}
