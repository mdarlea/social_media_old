using System;
using Swaksoft.Core;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services.Providers
{
    public class TwitterProviderAppService 
        : ProviderAppServiceBase<TwitterProviderAppService, TwitterUserProfile>, IProviderAppService
    {
        public TwitterProviderAppService(
            IOAuthAuthorizationAdapter ioAuthAuthorizationAdapter,
            IUserProfileAdapter userProfileAdapter,
            IUserRepository userRepository,
            IUserProfileRepository userProfileRepository)
            : base(ioAuthAuthorizationAdapter, userProfileAdapter, userRepository, userProfileRepository)
        {
        }

        protected override TwitterUserProfile CreateUserProfile(
            AccessTokenResult accessTokenResult, 
            UserProfileResult userProfileResult, 
            User user)
        {
            var token = new OAuthToken(accessTokenResult.AccessToken, accessTokenResult.AccessTokenSecret);

            //create a new profile
            var factory = new TwitterUserProfileFactory(user, token, userProfileResult.UserName,
                userProfileResult.ExternalUserId, userProfileResult.Name);

            return factory.CreateUserProfile();
        }
    }
}
