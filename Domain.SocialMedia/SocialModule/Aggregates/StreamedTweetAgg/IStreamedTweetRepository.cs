using System;
using System.Linq;
using System.Linq.Expressions;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.Seedwork.Specification;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg
{
    public interface IStreamedTweetRepository : IRepository<StreamedTweet>
    {
        IQueryable<StreamedTweetsFilter> GetTweets();

        IQueryable<StreamedTweet> GetFilteredTweets(Expression<Func<StreamedTweetsFilter, bool>> filter);

        IQueryable<StreamedTweet> AllTweetsMatching(ISpecification<StreamedTweetsFilter> specification);
    }
}
