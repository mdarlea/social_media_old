using System;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Contracts
{
    public interface IOAuthAuthorizationAdapter
    {
        Uri Authorize(OAuthOptions options);

        Uri Authenticate(OAuthOptions options);

        AccessTokenResult GetAccessToken(OAuthOptions options);
    }
}