using System;
using Swaksoft.Core.External;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamFilterAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Events;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Services
{
    public interface ITweetProcessorService
    {
        void ProcessStreamFilter(
            TwitterUserProfile userProfile,
            StreamFilter streamFilter,
            ExternalProviderCredentials clientCredentials,
            StreamedTweet streamedTweet,
            Location location,
            DateTime dateSent);
    }
}