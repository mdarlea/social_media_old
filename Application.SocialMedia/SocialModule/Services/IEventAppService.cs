using System;
using System.Threading.Tasks;
using Swaksoft.Application.SocialMedia.Dto;
using Swaksoft.Core.Dto;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public interface IEventAppService : IDisposable
    {
        Task<Dto.EventWithAddressResult> AddNewEventAsync(Dto.EventWithAddress @event);
        Task<Dto.EventWithAddressResult> UpdateEventWithAddress(Dto.EventWithAddress @event);
        Task<EventResult> UpdateEvent(Dto.Event @event);
        void RemoveEvent(int eventId);
        Task<Dto.EventWithAddressResult> FindEvent(int eventId);
        Task<Dto.CollectionActionResult<Dto.Event>> FindDailyEventsForUser(string userId);
        Task<Dto.CollectionActionResult<Dto.Event>> FindWeeklyEventsForUser(string userId);
        Task<Dto.CollectionActionResult<Dto.Event>> FindMonthlyEventsForUser(string userId);
        Task<Dto.CollectionActionResult<Dto.Event>> FindEventsInTimeRangeForUser(EventForUserRequest eventForUser);
        Task<Dto.CollectionActionResult<Dto.Event>> FindDailyEvents();
        Task<Dto.CollectionActionResult<Dto.Event>> FindWeeklyEvents();
        Task<Dto.CollectionActionResult<Dto.Event>> FindMonthlyEvents();
        Task<Dto.CollectionActionResult<Dto.Event>> FindEventsInTimeRange(TimeRange timeRange);
        Task<Dto.CollectionActionResult<Dto.EventWithAddress>> FindEventsInArea(Location location);
    }
}
