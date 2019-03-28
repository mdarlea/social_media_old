using System.Collections.Generic;
using System.Text;
using Swaksoft.Domain.Seedwork.Specification;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg
{
    public static class StreamedTweetSpecifications
    {
        public static ISpecification<StreamedTweetsFilter> FilteredStreamedTweets(StreamedTweetOptions options)
        {
            Specification<StreamedTweetsFilter> spec = new TrueSpecification<StreamedTweetsFilter>();

            var userProfileId = options.UserProfileId ?? 0;
            if (userProfileId > 0)
            {
                spec &= new DirectSpecification<StreamedTweetsFilter>(s => s.UserProfileId == options.UserProfileId);
            }

            if (!string.IsNullOrWhiteSpace(options.Query))
            {
                spec &= new DirectSpecification<StreamedTweetsFilter>(s => s.StreamedTweet.Query == options.Query);
            }

            if (!string.IsNullOrWhiteSpace(options.SearchText))
            {
                var searchText = options.SearchText;
                spec &= new DirectSpecification<StreamedTweetsFilter>(s => s.StreamedTweet.Text.Contains("" + searchText));
            }
            
            return spec;
        }

        public static DynamicSpecificationResult DynamicFilteredStreamedTweets(StreamedTweetOptions options)
        {
            var filter = new StringBuilder("true");
            var args = new List<object>();
            var profileId = options.UserProfileId ?? 0;
            var idx = 0;
            if (profileId > 0)
            {
                filter.Append(string.Format(" && (UserProfileId == @{0})", idx++));
                args.Add(options.UserProfileId);
            }
            if (!string.IsNullOrWhiteSpace(options.Query))
            {
                filter.Append(string.Format(" && (StreamedTweet.Query == @{0})", idx++));
                args.Add(options.Query);
            }
            if (!string.IsNullOrWhiteSpace(options.SearchText))
            {
                filter.Append(string.Format(" && StreamedTweet.Text.Contains(@{0})", idx));
                args.Add(options.SearchText);
            }

            return new DynamicSpecificationResult()
            {
                Filter = filter.ToString(),
                Parameters = args.ToArray()
            };
        }
    }
}
