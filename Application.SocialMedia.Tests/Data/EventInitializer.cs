using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.EventAgg;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace Application.SocialMedia.Tests.Data
{
    public partial class SocialMediaDatabaseInitializer
    {
        partial void SeedEvents(TestSocialMediaUnitOfWork context)
        {
            var homeAddress = context.Addresses.Find(1);
            var churchAddress = context.Addresses.Find(2);

            var user = context.Users.SingleOrDefault(u => u.Id == "ef4b2bdb-eda9-4778-bc1c-ab347a4924f5");
            
            var sunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek).AddHours(10);
            var result = sunday.CompareTo(DateTime.Now);
            if (result < 0)
            {
                sunday = sunday.AddDays(7);
            }
            var sundayStart = sunday;
            var sundayEnd = sundayStart.AddHours(2);

            var saturday = DateTime.Today.AddDays((int)DayOfWeek.Saturday - (int)DateTime.Today.DayOfWeek).AddHours(19);

            var saturdayStart = saturday;
            var saturdayEnd = saturdayStart.AddHours(2);

            context.Database.ExecuteSqlCommand("INSERT INTO Events(Name,Description,AddressId,StartTime,EndTime, UserId) values(@name,@description,@addressId,@startTime,@endTime,@userId)",
    new SqlCeParameter("@name", "Prayer Meeting"),
    new SqlCeParameter("@description", "Meet up for prayer"),
    new SqlCeParameter("@addressId", homeAddress.Id),
    new SqlCeParameter("@startTime", saturdayStart),
    new SqlCeParameter("@endTime", saturdayEnd),
    new SqlCeParameter("@userId", user.Id));

            var @event = new Event()
            {
                Name = "Church Service",
                Description = "Church service and prayeer",
                StartTime = sundayStart,
                EndTime = sundayEnd
            };
            @event.SetTheAddressForThisEvent(churchAddress);
            @event.SetTheUserForThisEvent(user);
            context.Events.Add(@event);

            context.SaveChanges();

            var startTime = new DateTime(2016, 12, 14);
            startTime = startTime.AddHours(13);
            var endTime = new DateTime(2016, 12, 14);
            endTime = endTime.AddHours(15);
            context.Database.ExecuteSqlCommand("INSERT INTO Events(Name,Description,AddressId,StartTime,EndTime, UserId, Repeat) values(@name,@description,@addressId,@startTime,@endTime,@userId,@repeat)",
                new SqlCeParameter("@name", "Small Group Meeting"),
                new SqlCeParameter("@description", "Small Group Meeting"),
                new SqlCeParameter("@addressId", homeAddress.Id),
                new SqlCeParameter("startTime", startTime),
                new SqlCeParameter("endTime", endTime),
                new SqlCeParameter("@userId", user.Id),
                new SqlCeParameter("@repeat", 1));
        }
    }
}
