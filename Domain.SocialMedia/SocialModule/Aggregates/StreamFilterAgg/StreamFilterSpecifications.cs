using System;
using Swaksoft.Domain.Seedwork.Specification;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamFilterAgg
{
    public static class StreamFilterSpecifications
    {
        public static Specification<StreamFilter> StreamFiltersByUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException("userId");

            return new DirectSpecification<StreamFilter>(qf => !qf.Disabled && qf.UserId == userId);
        }

        public static ISpecification<StreamFilter> StreamFiltersByQuery(string userId, string query)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException("userId");

            var specification = StreamFiltersByUserId(userId);
            return specification & new DirectSpecification<StreamFilter>(qf => qf.Query == query);
        }
    }
}
