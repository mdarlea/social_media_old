using System;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamFilterAgg
{
    public static class StreamFilterFactory
    {
        public static StreamFilter CreateStreamFilter(User user, string query)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(query)) throw new ArgumentNullException("query");

            var newQueryFilter = new StreamFilter
            {
                Query = query
            };
            newQueryFilter.SetUserForThisStreamFilter(user);

            return newQueryFilter;
        }
    }
}
