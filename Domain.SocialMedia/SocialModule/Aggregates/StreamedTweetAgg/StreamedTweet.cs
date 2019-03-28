using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.SentMessageAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg
{
    public class StreamedTweet : Entity
    {
        [Required]
        public long StatusId { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Name { get; set; }

        [Required]
        public string Text { get; set; }

        public long? InReplyToStatusId { get; set; }
        public string ProfileImageUrl { get; set; }
        public string ProfileImageUrlHttps { get; set; }

        public string Query { get; set; }

        private HashSet<SentTweet> _sentTweets;
        public virtual ICollection<SentTweet> SentTweets
        {
            get
            {
                return _sentTweets ?? (_sentTweets = new HashSet<SentTweet>());
            }
            set
            {
                _sentTweets = new HashSet<SentTweet>(value);
            }
        }
        
        private HashSet<TwitterUserProfile> _userProfiles;
        public virtual ICollection<TwitterUserProfile> UserProfiles
        {
            get
            {
                return _userProfiles ?? (_userProfiles = new HashSet<TwitterUserProfile>());
            }
            set
            {
                _userProfiles = new HashSet<TwitterUserProfile>(value);
            }
        }
    }
}
