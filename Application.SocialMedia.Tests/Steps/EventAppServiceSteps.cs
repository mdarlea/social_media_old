using System;
using System.Linq;
using Application.SocialMedia.Tests.Data;
using Application.SocialMedia.Tests.Extensions;
using Should;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Dto = Swaksoft.Application.SocialMedia.Dto;
using Domain = Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.EventAgg;
using Swaksoft.Core.Dto;

namespace Application.SocialMedia.Tests.Steps
{
    [Binding]
    public class EventAppServiceSteps
    {
        private readonly DataContext context;
        private Dto.EventWithAddressResult returnedEventResult;
        private Dto.CollectionActionResult<Dto.Event> returnedEventsResult;
        private int newEventId;

        public EventAppServiceSteps(DataContext context)
        {
            this.context = context;
        }

        [AfterScenario("CreateNewEventAtExistingAddress")]
        public void CreateNewEventAtExistingAddress()
        {
            using (var dbContext = new TestSocialMediaUnitOfWork())
            {
                var @event = dbContext.Events.Find(newEventId);
                dbContext.Events.Remove(@event);
                dbContext.SaveChanges();
            }
        }

        [Given(@"the following event:")]
        public void GivenTheFollowingEvent(Table table)
        {
            var startTime = table.GetDateTimeValue("StartTime");
            var endTime = table.GetDateTimeValue("EndTime");

            var dto = table.CreateInstance<Dto.EventWithAddress>();
            dto.StartTime = startTime;
            dto.EndTime = endTime;

            context.GivenEventDto = dto;
        }

        

        [When(@"I create this event")]
        public async void WhenICreateThisEvent()
        {
            using (var service = context.GetEventAppService())
            {
                returnedEventResult = await service.AddNewEventAsync(context.GivenEventDto);
            }
        }

        [Then(@"a new event with the information below should be created in the database:")]
        public void ThenANewEventWithTheInformationBelowShouldBeCreatedInTheDatabase(Table table)
        {
            var expectedEvent = table.CreateInstance<Domain.Event>();
            expectedEvent.StartTime = table.GetDateTimeValue("StartTime");
            expectedEvent.EndTime = table.GetDateTimeValue("EndTime");

            using (var dbcontext = new TestSocialMediaUnitOfWork())
            {
                var persistedEvent = dbcontext.Events.FirstOrDefault(e =>
                    e.Name == expectedEvent.Name &&
                    e.Description == expectedEvent.Description &&
                    e.AddressId == expectedEvent.AddressId &&
                    e.UserId == expectedEvent.UserId &&
                    e.StartTime == expectedEvent.StartTime &&
                    e.EndTime == expectedEvent.EndTime);

                persistedEvent.ShouldNotBeNull();
                newEventId = persistedEvent.Id;
            }
        }

        [Then(@"the event application service should return a dto with the following event information:")]
        public void ThenTheEventApplicationServiceShouldReturnADtoWithTheFollowingEventInformation(Table table)
        {
            var expectedEvent = table.CreateInstance<Dto.EventWithAddressResult>();
            expectedEvent.StartTime = table.GetDateTimeValue("StartTime");
            expectedEvent.EndTime = table.GetDateTimeValue("EndTime");

            returnedEventResult.Status.ShouldEqual(expectedEvent.Status);
            returnedEventResult.Message.ShouldEqual(expectedEvent.Message);
            returnedEventResult.Name.ShouldEqual(expectedEvent.Name);
            returnedEventResult.AddressId.ShouldEqual(expectedEvent.AddressId);
            returnedEventResult.UserId.ShouldEqual(expectedEvent.UserId);

            if (expectedEvent.StartTime != DateTime.MinValue)
            {
                returnedEventResult.StartTime.ShouldEqual(expectedEvent.StartTime);
            }

            if (expectedEvent.EndTime != DateTime.MinValue)
            {
                returnedEventResult.EndTime.ShouldEqual(expectedEvent.EndTime);
            }

            returnedEventResult.Instructor.ShouldEqual(expectedEvent.Instructor);
        }

        [Then(@"the followng address:")]
        public void ThenTheFollowngAddress(Table table)
        {
            var expectedDto = table.CreateInstance<Dto.Address>();

            returnedEventResult.Address.ShouldNotBeNull();

            var dto = returnedEventResult.Address;
            
            dto.StreetAddress.ShouldEqual(expectedDto.StreetAddress);
            (dto.SuiteNumber??string.Empty).ShouldEqual(expectedDto.SuiteNumber??string.Empty);
            dto.City.ShouldEqual(expectedDto.City);
            dto.State.ShouldEqual(expectedDto.State);
            dto.Zip.ShouldEqual(expectedDto.Zip);
            dto.GeolocationStreet.ShouldEqual(expectedDto.GeolocationStreet);
            dto.GeolocationStreetNumber.ShouldEqual(expectedDto.GeolocationStreetNumber);
            dto.Latitude.ShouldEqual(expectedDto.Latitude);
            dto.Longitude.ShouldEqual(expectedDto.Longitude);
            dto.CountryIsoCode.ShouldEqual(expectedDto.CountryIsoCode);
        }


        [When(@"I search for the user's events in the weekly calendar")]
        public async void WhenISearchForTheUserSEventsInTheWeeklyCalendar()
        {
            using (var service = context.GetEventAppService())
            {
                returnedEventsResult = await service.FindWeeklyEventsForUser(context.GivenUserId);
            }
        }
        
        [Then(@"the service should return the following events:")]
        public void ThenTheServiceShouldReturnTheFollowingEvents(Table table)
        {
            var expectedEvents = table.CreateSet<Dto.Event>().ToList();
            expectedEvents.Count.ShouldEqual(returnedEventsResult.Items.Count);
            foreach (var @event in returnedEventsResult.Items)
            {
                var expectedEvent = expectedEvents.FirstOrDefault(e =>
                    e.Name == @event.Name &&
                    e.Description == (@event.Description ?? string.Empty) &&
                    e.AddressId == @event.AddressId &&
                    e.UserId == @event.UserId &&
                    e.Instructor == @event.Instructor);
                expectedEvent.ShouldNotBeNull();
            }
        }

        [Then(@"the '(.*)' event should start on (.*) from (.*) until (.*)")]
        public void ThenTheEventShouldStartOnSaturdayFromUntil(string p0, string p1, string p2, string p3)
        {
            var @event = returnedEventsResult.Items.FirstOrDefault(e => e.Name == p0);
            @event.ShouldNotBeNull();
            var dayOfWeek=0;
            if (p1.ToUpper() == "SATURDAY")
            {
                dayOfWeek = (int) DayOfWeek.Saturday;
            }

            var date = DateTime.Today.AddDays(dayOfWeek - (int)DateTime.Today.DayOfWeek);
            var startTime = date.GetDateTimeValue(p2);
            var endTime = date.GetDateTimeValue(p3);

            @event.StartTime.ShouldEqual(startTime);
            @event.EndTime.ShouldEqual(endTime);
        }

        [Given(@"the event id equal with (.*)")]
        public void GivenTheEventIdEqualWith(int p0)
        {
            context.GivenEventId = p0;
        }

        [When(@"I search for this event")]
        public async void WhenISearchForThisEvent()
        {
            using (var service = context.GetEventAppService())
            {
                returnedEventResult = await service.FindEvent(context.GivenEventId);
            }
        }

        [Given(@"a start time equal with (.*)")]
        public void GivenAStartTimeEqualWith(DateTime startTime)
        {
            if (context.GivenEventForUserDto == null)
            {
                context.GivenEventForUserDto = new Dto.EventForUserRequest();
            }
            if (context.GivenEventForUserDto.TimeRange == null)
            {
                context.GivenEventForUserDto.TimeRange = new Dto.TimeRange();
            }
            context.GivenEventForUserDto.TimeRange.StartTime = startTime;
        }

        [Given(@"an end time equal with (.*)")]
        public void GivenAnEndTimeEqualWith(DateTime endTime)
        {
            if (context.GivenEventForUserDto == null)
            {
                context.GivenEventForUserDto = new Dto.EventForUserRequest();
            }
            if (context.GivenEventForUserDto.TimeRange == null)
            {
                context.GivenEventForUserDto.TimeRange = new Dto.TimeRange();
            }
            context.GivenEventForUserDto.TimeRange.EndTime = endTime;
        }

        [When(@"I search for the user's events in the given time range")]
        public async void WhenISearchForTheUserSEventsInTheGivenTimeRange()
        {
            using (var service = context.GetEventAppService())
            {
                returnedEventsResult = await service.FindEventsInTimeRangeForUser(context.GivenEventForUserDto);
            }
        }

        [Then(@"the application service should return an action result with the (.*) status")]
        public void ThenTheApplicationServiceShouldAnActionResultWithTheStatus(ActionResultCode p0)
        {
            returnedEventsResult.Status.ShouldEqual(p0);
        }

        [Then(@"the following event should be found:")]
        public void ThenTheFollowingEventShouldBeFound(Table table)
        {
            var @event = table.CreateInstance<Dto.Event>();

            var expectedEvent = returnedEventsResult.Items.SingleOrDefault(e =>
                e.Id == @event.Id &&
                e.Name == @event.Name &&
                e.Description == (@event.Description ?? string.Empty) &&
                e.Instructor == @event.Instructor &&
                e.AddressId == @event.AddressId &&
                e.UserId == @event.UserId &&
                e.Repeat == @event.Repeat &&
                e.StartTime == @event.StartTime &&
                e.EndTime == @event.EndTime);

            expectedEvent.ShouldNotBeNull();
        }

        [AfterScenario("FindRepeatedEvents")]
        public void FindRepeatedEvents()
        {
            this.context.GivenEventForUserDto = null;
        }

    }
}
