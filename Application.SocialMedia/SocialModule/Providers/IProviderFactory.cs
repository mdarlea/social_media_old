using System;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;

namespace Swaksoft.Application.SocialMedia.SocialModule.Providers
{
    public interface IProviderFactory
    {
        UserProfile CreateUserProfile(IUserProfileRepository repository, string userId);
    }

    public interface IProviderFactory<T> : IProviderFactory
        where T : UserProfile
    {
    }
}
