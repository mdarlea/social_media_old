using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Application.SocialMedia.Tests.Extensions
{
    public static class TableExtensions
    {
        public static DateTime GetDateTimeValue(this Table table, string name)
        {
            foreach (var row in table.Rows)
            {
                if (row[0] != name) continue;

                var value = row[1].Trim().ToUpper();
                return DateTime.Today.GetDateTimeValue(value);
            }
            return DateTime.MinValue;
        }

        public static DateTime GetDateTimeValue(this DateTime date, string value)
        {
            var isPM = value.Substring(value.Length - 2) == "PM";
            var isAM = value.Substring(value.Length - 2) == "PM";
            
            string timeText = null;
            if (isPM || isAM)
            {
                timeText = value.Substring(0, value.Length - 2).Trim();
            }
            else
            {
                timeText = value;
            }
            int time;
            int.TryParse(timeText, out time);
            var hours = time;
            if (isPM)
            {
                hours += 12;
            }
            date = date.AddHours(hours);
            return date;
        }
    }
}
