using System;
using Swaksoft.Core;
using Swaksoft.Core.External;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Contracts
{
    public interface IUserProfileAdapter
    {
        ExternalProvider Provider { get; }

        UserProfileResult GetUserProfile(ExternalProviderCredentials request, OAuthToken token);

        UserProfileResult GetUserProfile(ExternalProviderCredentials request, string accessToken, string accessTokenSecret);
    }
}
