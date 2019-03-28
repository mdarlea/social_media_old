using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Swaksoft.Core;
using Swaksoft.Core.External;
using Swaksoft.Domain.Seedwork.Extensions;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.SentMessageAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg
{
    public class TwitterUserProfile : UserProfile
    {
        public TwitterUserProfile()
        {
            ProviderKey = ExternalProvider.Twitter;
        }

        [Required]
        public long TwitterUserId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        private HashSet<StreamedTweet> _streamedTweets;
        public virtual ICollection<StreamedTweet> StreamedTweets
        {
            get
            {
                return _streamedTweets ?? (_streamedTweets = new HashSet<StreamedTweet>());
            }
            set
            {
                _streamedTweets = new HashSet<StreamedTweet>(value);
            }
        }
        
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = base.Validate(validationContext);

            return this.ValidationResults(results)
                .NotNullOrEmpty(e => e.Name)
                .Validate(e => e.TwitterUserId, value => value < 1, @"Invalid Twitter UserId")
                .Execute();
        }

        public bool MessageWasSent(string sentToUserName, string message)
        {
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException("message");

            var msg = (from e in SentMessages.OfType<SentTweet>()
                where (e.SentToUserName == sentToUserName && e.MessageSent == message)
                select e).FirstOrDefault();

            return (msg != null);
        }
    }
}
