using System;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public interface IProviderAppService : IDisposable
    {
        Dto.UserProfileResult Connect(Dto.OAuthRequest request);
    }
}
