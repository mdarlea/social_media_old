using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.Seedwork.Specification;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.EventAgg
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<IList<Event>> GetEventsWithAddressMatching(ISpecification<Event> specification);
        Task<IList<Event>> GetEventsWithUserMatching(ISpecification<Event> specification);
        Task<Event> GetEventWithAddressAndUser(int eventId);
        Task<IList<Event>> FindAllInArea(double longitude, double latitude, int meters);
    }
}
