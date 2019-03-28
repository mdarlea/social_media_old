using System;
using Swaksoft.Domain.Seedwork.Events;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Events
{
    public class UserProfileRemoved : IDomainEvent
    {
        public int UserProfileId { get; private set; }

        public UserProfileRemoved(int userProfileId)
        {
            if (userProfileId < 1) throw new ArgumentException("userProfileId");
            UserProfileId = userProfileId;
        }
    }
}
