using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg
{
    public class FoursquareUserProfileFactory : UserProfileFactory<FoursquareUserProfile>
    {
        public FoursquareUserProfileFactory(User user, OAuthToken authorizationToken, string userName, string userId) 
            : base(user, authorizationToken, userName, userId)
        {
        }
    }
}
