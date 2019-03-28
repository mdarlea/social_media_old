using System;
using System.ComponentModel.DataAnnotations;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg
{
    public class BlockedUser : Entity
    {
        [Required]
        public string UserName { get; set; }
        
        public long? UserId { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
