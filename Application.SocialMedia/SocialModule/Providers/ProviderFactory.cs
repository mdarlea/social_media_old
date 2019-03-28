using System;
using System.Linq;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;

namespace Swaksoft.Application.SocialMedia.SocialModule.Providers
{
    public class ProviderFactory<T> : IProviderFactory<T> 
        where T:UserProfile
    {
        public UserProfile CreateUserProfile(IUserProfileRepository repository, string userId)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException("userId");

            return repository.GetByUserId<T>(userId).SingleOrDefault();
        }
    }
}
