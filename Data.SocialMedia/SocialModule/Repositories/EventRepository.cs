using System;
using System.Collections.Generic;
using System.Linq;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Specification;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.EventAgg;
using Swaksoft.Infrastructure.Data.Seedwork.Repositories;
using System.Data.Entity;
using System.Threading.Tasks;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;

namespace Swaksoft.Infrastructure.Data.SocialMedia.SocialModule.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        private class EventWithAddress
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int AddressId { get; private set; }
            public virtual DateTime StartTime { get; set; }
            public virtual DateTime EndTime { get; set; }
            public bool? IsDeleted { get; private set; }
            public string UserId { get; private set; }
            public bool? Repeat { get; set; }
            public string StreetAddress { get; set; }
            public string SuiteNumber { get; set; }
            public int GeolocationStreetNumber { get; set; }
            public string GeolocationStreet { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public string CountryIsoCode { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public bool IsMainAddress { get; set; }
            public int? AddressTypeId { get; set; }
            public string Name_FirstName { get; set; }
            public string Name_MiddleName { get; set; }
            public string Name_LastName { get; set; }
			public string RecurrencePattern { get; set; }
			public string RecurrenceException { get; set; }
        }

        public EventRepository(ITransactionUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IList<Event>> GetEventsWithAddressMatching(ISpecification<Event> specification)
        {
            return await Context.Set<Event>()
                .Include(e => e.Address)
                .Where(specification.SatisfiedBy())
                .ToListAsync();
        }

        public async Task<IList<Event>> GetEventsWithUserMatching(ISpecification<Event> specification)
        {
            return await Context.Set<Event>()
                .Include(e => e.User)
                .Where(specification.SatisfiedBy())
                .ToListAsync();
        }

        public async Task<Event> GetEventWithAddressAndUser(int eventId)
        {
            return await GetSet()
                .Include(e => e.Address)
                .Include(e => e.User)
                .Where(e => e.IsDeleted == null || !e.IsDeleted.Value)
                .FirstOrDefaultAsync(e => e.Id == eventId);
        }

        public async Task<IList<Event>> FindAllInArea(double longitude, double latitude, int meters)
        {
            var list =
                await
                    Context.Database.SqlQuery<EventWithAddress>("CALL EventsInArea({0}, {1}, {2})", latitude,longitude,
                        meters).ToListAsync();
            var events = new List<Event>();
            foreach (var e in list)
            {
                var address = AddressFactory.CreateAddress(e.StreetAddress,
                    e.SuiteNumber,
                    e.City,
                    e.State,
                    e.Zip,
                    e.Latitude,
                    e.Longitude,
                    e.GeolocationStreetNumber,
                    e.GeolocationStreet,
                    e.CountryIsoCode,
                    e.IsMainAddress);
                if (e.AddressTypeId != null)
                {
                    address.SetTheAddressTypeReference(e.AddressTypeId.Value);
                }
                address.ChangeCurrentIdentity(e.AddressId);

                var user = new User
                {
                    Name = new Name(e.Name_FirstName, e.Name_LastName, e.Name_MiddleName)
                };
                user.ChangeCurrentIdentity(e.UserId);

                var @event = EventFactory.CreateEvent(e.Name, e.Description, e.StartTime, e.EndTime, address, user, e.RecurrencePattern, e.RecurrenceException);
                @event.ChangeCurrentIdentity(e.Id);

                events.Add(@event);
            }

            return events;
        }
    }
}
