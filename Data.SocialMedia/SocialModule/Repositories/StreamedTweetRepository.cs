using System;
using System.Linq;
using System.Linq.Expressions;
using Swaksoft.Domain.Seedwork;
using Swaksoft.Domain.Seedwork.Specification;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg;
using Swaksoft.Infrastructure.Data.Seedwork.Repositories;

namespace Swaksoft.Infrastructure.Data.SocialMedia.SocialModule.Repositories
{
    public class StreamedTweetRepository : Repository<StreamedTweet>, IStreamedTweetRepository
    {
        public StreamedTweetRepository(ITransactionUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public IQueryable<StreamedTweet> GetFilteredTweets(Expression<Func<StreamedTweetsFilter, bool>> filter)
        {
            if (filter == null) throw new ArgumentNullException("filter");
            return GetTweets().Where(filter).Select(e => e.StreamedTweet);
        }

        public IQueryable<StreamedTweet> AllTweetsMatching(ISpecification<StreamedTweetsFilter> specification)
        {
            if (specification == null) throw new ArgumentNullException("specification");
            return GetFilteredTweets(specification.SatisfiedBy());
        }

        public IQueryable<StreamedTweetsFilter> GetTweets()
        {
            return GetQuery().SelectMany(
                s => s.UserProfiles,
                (s, u) =>
                    new StreamedTweetsFilter
                    {
                        StreamedTweet = s,
                        UserProfileId = u.Id
                    });
        }
    }
}
