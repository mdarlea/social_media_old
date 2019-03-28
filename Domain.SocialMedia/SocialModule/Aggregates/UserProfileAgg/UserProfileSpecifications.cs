using System;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.Seedwork.Specification;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg
{
    public static class UserProfileSpecifications
    {
        public static ISpecification<T> GetAllEnabled<T>()
            where T : UserProfile
        {
            return new DirectSpecification<T>(p => (p.Disabled == null || !(bool)p.Disabled));
        } 

        public static ISpecification<UserProfile> UserProfileByAuthorizationToken(OAuthToken authorizationToken)
        {
            if (authorizationToken == null) throw new ArgumentNullException("authorizationToken");

            return new DirectSpecification<UserProfile>(p => p.AuthorizationToken.Equals(authorizationToken));
        }

        public static ISpecification<T> UserProfileById<T>(int id)
            where T:UserProfile
        {
            if (id < 1) throw new ArgumentNullException("id");

            return new DirectSpecification<T>(tp => tp.Id == id);
        }

        public static ISpecification<T> UserProfileByUserId<T>(string userId)
            where T:UserProfile
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException("userId");

            return new DirectSpecification<T>(p => p.UserId == userId);
        }

        public static ISpecification<TwitterUserProfile> UserProfileByTwitterUserId(long userId)
        {
            if (userId < 1) throw new ArgumentNullException("userId");

            return new DirectSpecification<TwitterUserProfile>(tp => tp.TwitterUserId == userId);
        }
    }
}
