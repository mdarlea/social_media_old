using System;
using System.Threading.Tasks;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public interface IRealTimeStreamingAppService : IDisposable
    {
        Task SubscribeForStreaming(Dto.OAuthRequest request);
    }
}