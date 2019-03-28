using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamFilterAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamingEventAgg;
using Swaksoft.Infrastructure.Data.SocialMedia.UnitOfWork;

namespace Swaksoft.Infrastructure.Data.SocialMedia.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SocialMediaUnitOfWorkMySql>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SocialMediaUnitOfWorkMySql context)
        {
            //const string userId = "3b157eac-1faf-4de7-881a-2993996207ae";
           
            //var streamFilter1 = AddStreamFilter(context, userId, "@technology1976");
            //var streamFilter2 = AddStreamFilter(context, userId, "#test_michelle_streaming");
            //var streamFilter3 = AddStreamFilter(context, userId, "#michelle_streaming");

            //var @event = new StreamingEvent
            //{
            //    Code = StreamingEventType.DisplayActivityOnMap,
            //    Description = "Display activity on map",
            //    StreamFilters = new HashSet<StreamFilter> { streamFilter1, streamFilter2 }
            //};
            //context.StreamingEvents.AddOrUpdate(e => e.Code, @event);

            //context.SaveChanges();
        }

        private static StreamFilter AddStreamFilter(SocialMediaUnitOfWork context, string userId, string query)
        {
            var streamFilter = context.StreamFilters.FirstOrDefault(e => e.UserId == userId && e.Query == query);
            if (streamFilter != null) return streamFilter;

            streamFilter = new StreamFilter
            {
                UserId = userId,
                Query = query
            };
            context.StreamFilters.Add(streamFilter);
            return streamFilter;
        }
    }
}
