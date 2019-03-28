using System;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public interface IOAuthAppService : IDisposable
    {
        Dto.OAuthUrlResult Authorize(Dto.OAuthRequest request);
        Dto.OAuthUrlResult Authenticate(Dto.OAuthRequest request);
        Dto.AccessTokenResult GetAccessToken(Dto.OAuthRequest request);
    }
}
