using System;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.SentMessageAgg
{
    public abstract class SentMessageFactory<T>
        where T : SentMessage, new()
    {
        private readonly UserProfile _userProfile;
        private readonly DateTime _dateSent;
        private readonly string _messageSent;


        protected SentMessageFactory(UserProfile userProfile, DateTime dateSent, string messageSent)
        {
            if (userProfile == null) throw new ArgumentNullException("userProfile");
            if (string.IsNullOrWhiteSpace(messageSent)) throw new ArgumentNullException("messageSent");

            _userProfile = userProfile;
            _dateSent = dateSent;
            _messageSent = messageSent;
        }

        public virtual T CreateSentMessage()
        {
            return new T
            {
                UserProfile = _userProfile,
                UserProfileId = _userProfile.Id,
                DateSent = _dateSent,
                MessageSent = _messageSent
            };
        }
    }


}
