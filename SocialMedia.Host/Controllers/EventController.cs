using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Swaksoft.Application.SocialMedia.Dto;
using Swaksoft.Application.SocialMedia.SocialModule.Services;
using Swaksoft.Core.Dto;

namespace SocialMedia.Host.Controllers
{

    [RoutePrefix("api/event")]
    [Authorize]
    public class EventController : ApiControllerBase
    {
        private readonly IEventAppService eventAppService;

        public EventController(IEventAppService eventAppService)
        {
            if (eventAppService == null) throw new ArgumentNullException(nameof(eventAppService));
            this.eventAppService = eventAppService;
        }

        [HttpPost]
        [Route("FindEventsInArea")]
        public async Task<IHttpActionResult> FindEventsInArea(Location location)
        {
            if (location == null) throw new ArgumentNullException(nameof(location));
            var result = await eventAppService.FindEventsInArea(location);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return result.Status != ActionResultCode.Success
                ? GetErrorResult(result)
                : Ok(result.Items);
        }

        [HttpPost]
        [Route("AddNewEvent")]
        public async Task<IHttpActionResult> AddNewEvent(EventWithAddress @event)
        {
            @event.UserId = User.Identity.GetUserId();
            @event.AddressId = @event.Address?.Id ?? -1;

            var result = await eventAppService.AddNewEventAsync(@event);

            return result.Status != ActionResultCode.Success 
                ? GetErrorResult(result) 
                : Ok(result);
        }

        [HttpPost]
        [Route("RemoveEvent")]
        public IHttpActionResult RemoveEvent([FromBody]Event @event)
        {
            if (@event == null || @event.Id < 1) throw new ArgumentNullException(nameof(@event));

            eventAppService.RemoveEvent(@event.Id);
            return Ok(new ActionResult()
            {
                Status = ActionResultCode.Success
            });
        }

        [HttpPost]
        [Route("UpdateEventWithAddress")]
        public async Task<IHttpActionResult> UpdateEventWithAddress([FromBody]EventWithAddress @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            var result = await eventAppService.UpdateEventWithAddress(@event);

            return result.Status != ActionResultCode.Success
               ? GetErrorResult(result)
               : Ok(result);
        }

        [HttpPost]
        [Route("UpdateEvent")]
        public async Task<IHttpActionResult> UpdateEvent([FromBody]Event @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            var result = await eventAppService.UpdateEvent(@event);

            return result.Status != ActionResultCode.Success
               ? GetErrorResult(result)
               : Ok(result);
        }

        [HttpGet]
        [Route("FindEvent/{eventId}")]
        public async Task<IHttpActionResult> FindEvent(int eventId)
        {
            var result = await eventAppService.FindEvent(eventId);
            return result.Status != ActionResultCode.Success
              ? GetErrorResult(result)
              : Ok(result);
        }
        [HttpGet]
        [Route("FindDailyEventsForCurrentUser")]
        public async Task<IHttpActionResult> FindDailyEventsForCurrentUser()
        {
            var userId = User.Identity.GetUserId();
            var result = await eventAppService.FindDailyEventsForUser(userId);
            return result.Status != ActionResultCode.Success
               ? GetErrorResult(result)
               : Ok(result.Items);
        }

        [HttpGet]
        [Route("FindWeeklyEventsForCurrentUser")]
        public async Task<IHttpActionResult> FindWeeklyEventsForCurrentUser()
        {
            var userId = User.Identity.GetUserId();
            var result = await eventAppService.FindWeeklyEventsForUser(userId);
            return result.Status != ActionResultCode.Success
               ? GetErrorResult(result)
               : Ok(result.Items);
        }
        [HttpGet]
        [Route("FindMonthlyEventsForCurrentUser")]
        public async Task<IHttpActionResult> FindMonthlyEventsForCurrentUser()
        {
            var userId = User.Identity.GetUserId();
            var result = await eventAppService.FindMonthlyEventsForUser(userId);
            return result.Status != ActionResultCode.Success
               ? GetErrorResult(result)
               : Ok(result.Items);
        }

        [HttpGet]
        [Route("FindDailyEvents")]
        public async Task<IHttpActionResult> FindDailyEvents()
        {
            var result = await eventAppService.FindDailyEvents();
            return result.Status != ActionResultCode.Success
               ? GetErrorResult(result)
               : Ok(result.Items);
        }

        [HttpGet]
        [Route("FindWeeklyEvents")]
        public async Task<IHttpActionResult> FindWeeklyEvents()
        {
            var result = await eventAppService.FindWeeklyEvents();
            return result.Status != ActionResultCode.Success
               ? GetErrorResult(result)
               : Ok(result.Items);
        }
        [HttpGet]
        [Route("FindMonthlyEvents")]
        public async Task<IHttpActionResult> FindMonthlyEvents()
        {
            var result = await eventAppService.FindMonthlyEvents();
            return result.Status != ActionResultCode.Success
               ? GetErrorResult(result)
               : Ok(result.Items);
        }

        [HttpPost]
        [Route("FindEventsInTimeRange")]
        public async Task<IHttpActionResult> FindEventsInTimeRange(TimeRange timeRange)
        {
            if (timeRange == null) throw new ArgumentNullException(nameof(timeRange));
            var result = await eventAppService.FindEventsInTimeRange(timeRange);
            return result.Status != ActionResultCode.Success
               ? GetErrorResult(result)
               : Ok(result.Items);
        }

        [HttpPost]
        [Route("FindEventsInTimeRangeForUser")]
        public async Task<IHttpActionResult> FindEventsInTimeRangeForUser(TimeRange timeRange)
        {
            if (timeRange == null) throw new ArgumentNullException(nameof(timeRange));

            var userId = User.Identity.GetUserId();

            var result = await eventAppService.FindEventsInTimeRangeForUser(new EventForUserRequest {
                UserId = userId,
                TimeRange = timeRange
            });
            return result.Status != ActionResultCode.Success
               ? GetErrorResult(result)
               : Ok(result.Items);
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                eventAppService.Dispose();
            }
        }
    }
}
