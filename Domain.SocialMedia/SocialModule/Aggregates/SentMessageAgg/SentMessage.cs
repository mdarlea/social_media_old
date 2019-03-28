using System;
using System.ComponentModel.DataAnnotations;
using Swaksoft.Domain.Seedwork.Aggregates;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.SentMessageAgg
{
    public class SentMessage : Entity
    {
        public int UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        [Required]
        public DateTime DateSent { get; set; }

        [Required]
        public string MessageSent { get; set; }
    }
}
