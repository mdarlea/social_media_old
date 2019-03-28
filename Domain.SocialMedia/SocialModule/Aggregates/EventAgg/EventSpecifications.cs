using System;
using Swaksoft.Domain.Seedwork.Specification;
using System.Data.Entity;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.EventAgg
{
    public static class EventSpecifications
    {
        public static Specification<Event> ActiveEvents()
        {
            return new DirectSpecification<Event>(e=>e.IsDeleted==null || !e.IsDeleted.Value);
        }
        public static ISpecification<Event> DailyEventsForUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));

            var userSpec = new DirectSpecification<Event>(e => e.UserId == userId);
            return userSpec && DailyEvents();
        }
        
        public static Specification<Event> DailyEvents()
        {
            var today = DateTime.Today;
            var tomorrow = DateTime.Today.AddDays(1);
            var dateSpec = new DirectSpecification<Event>(e => e.StartTime >= today && e.EndTime < tomorrow);

            return ActiveEvents() && dateSpec;
        }

        public static ISpecification<Event> WeeklyEventsForUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));

            var userSpec = new DirectSpecification<Event>(e=>e.UserId==userId);

            return userSpec &&  WeeklyEvents();
        }

        public static Specification<Event> WeeklyEvents()
        {
            var sunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            var nextSunday = DateTime.Today.AddDays((int)DayOfWeek.Saturday - (int)DateTime.Today.DayOfWeek +1);
            var timeSpec = new DirectSpecification<Event>(e =>
                e.StartTime >= sunday && e.StartTime < nextSunday &&
                e.EndTime >= sunday && e.EndTime < nextSunday);

			// var date = sunday.Date;
			// var repeatSpec = new DirectSpecification<Event>(e => e.Repeat.Value == true)
			//				& new DirectSpecification<Event>(e => date >= DbFunctions.TruncateTime(e.StartTime));
			
			var repeatSpec = new DirectSpecification<Event>(e => e.Repeat.Value == true);

            return ActiveEvents() && new OrSpecification<Event>(timeSpec, repeatSpec);
        }		

        public static ISpecification<Event> MonthlyEventsForUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));

            var userSpec = new DirectSpecification<Event>(e => e.UserId == userId);
            return userSpec && MonthlyEvents();
        }

        public static Specification<Event> MonthlyEvents()
        {
            var year = DateTime.Today.Year;
            var month = DateTime.Today.Month;
            var days = DateTime.DaysInMonth(year, month);
            var startTime = new DateTime(year, month, 1);
            var endTime = new DateTime(year, month, days);
            return EventsInTimeRange(startTime, endTime);
        }

        public static ISpecification<Event> EventsInTimeRangeForUser(string userId, DateTime startTime, DateTime endTime)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));

            var userSpec = new DirectSpecification<Event>(e => e.UserId == userId);
            return userSpec && EventsInTimeRange(startTime, endTime);
        }

        public static Specification<Event> EventsInTimeRange(DateTime startTime, DateTime endTime)
        {
			var endDateTime = endTime; // + TimeSpan.FromDays(1);
            var spec = new DirectSpecification<Event>(e => e.StartTime >= startTime && e.EndTime <= endDateTime);

			var repeatSpec = new DirectSpecification<Event>(e => e.Repeat.Value == true);

			return ActiveEvents() && new OrSpecification<Event>(spec, repeatSpec);
		}

        public static Specification<Event> EventsRepeatedWeekly(DateTime startTime, DateTime endTime)
        {
            var dateStart = startTime.Date;
            Specification<Event> spec =
                new DirectSpecification<Event>(e => (dateStart >= DbFunctions.TruncateTime(e.StartTime)) && (DbFunctions.DiffDays(dateStart, DbFunctions.TruncateTime(e.StartTime)) % 7 == 0));


            var date = startTime.Date + TimeSpan.FromDays(1);
            var i = 1;
            while (date <= endTime.Date)
            {
                var dt = startTime.Date + TimeSpan.FromDays(i);
                var dateSpec =
                    new DirectSpecification<Event>(e => (dt >= DbFunctions.TruncateTime(e.StartTime)) && (DbFunctions.DiffDays(dt, DbFunctions.TruncateTime(e.StartTime)) % 7 == 0));

                spec = new OrSpecification<Event>(spec, dateSpec);
                date = date + TimeSpan.FromDays(1);
                i++;
            }
            return new DirectSpecification<Event>(e => e.Repeat.Value == true)
                   & spec;
        }       
    }
}
