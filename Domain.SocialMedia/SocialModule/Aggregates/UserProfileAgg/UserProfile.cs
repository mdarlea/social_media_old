using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using Swaksoft.Core;
using Swaksoft.Core.External;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.Seedwork.Aggregates.ValueObjects;
using Swaksoft.Domain.Seedwork.Extensions;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.SentMessageAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg
{
    /// <summary>
    /// Aggregate Root
    /// </summary>
    public class UserProfile : Entity, IValidatableObject
    {
        [Required]
        public OAuthToken AuthorizationToken { get; set; }

        [Required]
        public string UserName { get; set; }

        public string ExternalUserId { get; set; }

        public bool? Disabled { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
        
        private HashSet<SentMessage> _sentMessages;
        public virtual ICollection<SentMessage> SentMessages
        {
            get
            {
                return _sentMessages ?? (_sentMessages = new HashSet<SentMessage>());
            }
            set
            {
                _sentMessages = new HashSet<SentMessage>(value);
            }
        }

        private HashSet<BlockedUser> _blockedUsers;
        public virtual ICollection<BlockedUser> BlockedUsers
        {
            get
            {
                return _blockedUsers ?? (_blockedUsers = new HashSet<BlockedUser>());
            }
            set
            {
                _blockedUsers = new HashSet<BlockedUser>(value);
            }
        }

        public virtual bool IsUserBlocked(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException("userName");

            var name = userName.ToLower();
            var user = (from u in BlockedUsers
                where u.UserName.ToLower() == name
                select u).FirstOrDefault();
            
            return (user != null);
        }
        
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return this.ValidationResults()
                .NotNull(e => e.AuthorizationToken)
                .NotNullOrEmpty(e => UserName)
                .Execute();
        }

        [NotMapped]
        public ExternalProvider ProviderKey { get; protected set; }
    }
}
