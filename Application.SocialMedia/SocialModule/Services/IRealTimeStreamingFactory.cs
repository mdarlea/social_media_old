using System;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public interface IRealTimeStreamingFactory : IDisposable
    {
        StreamingOptions GetStreamingOptionsFor<TProfile>(Dto.OAuthRequest request)
            where TProfile : UserProfile;
    }
}