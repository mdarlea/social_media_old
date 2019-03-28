using System;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg
{
    public class TwitterUserProfileFactory : UserProfileFactory<TwitterUserProfile>
    {
        private readonly string _name;

        public TwitterUserProfileFactory(User user, OAuthToken authorizationToken, string userName, string userId, string name)
            : base(user, authorizationToken, userName, userId)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");
            _name = name;
        }

        public override TwitterUserProfile CreateUserProfile()
        {
            var profile = base.CreateUserProfile();

            long twitterUserId;
            long.TryParse(UserId, out twitterUserId);

            //profile.Provider = "Twitter";
            profile.TwitterUserId = twitterUserId;
            profile.Name = _name;

            return profile;
        }
    }
}
