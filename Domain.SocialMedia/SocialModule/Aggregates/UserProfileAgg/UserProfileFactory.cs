using System;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg
{
    public abstract class UserProfileFactory<T>
        where T : UserProfile, new()
    {
        private readonly User _user;
        private readonly string _userName;
        private readonly OAuthToken _authorizationToken;
        private readonly string _userId;

        protected UserProfileFactory(User user, OAuthToken authorizationToken, string userName, string userId)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException("userName");
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException("userId");
            if (authorizationToken == null) throw new ArgumentNullException("authorizationToken");

            _user = user;
            _userName = userName;
            _userId = userId;
            _authorizationToken = authorizationToken;
        }

        protected string UserId
        {
            get { return _userId; }
        }

        public virtual T CreateUserProfile()
        {
            var profile = new T
            {
                //User = _user,
                UserId = _user.Id,
                UserName = _userName,
                ExternalUserId = _userId,
                AuthorizationToken = _authorizationToken,
                Disabled = false
            };

            if (!_user.IsTransient())
            {
                profile.UserId = _user.Id;
            }

            return profile;
        }
    }


}
