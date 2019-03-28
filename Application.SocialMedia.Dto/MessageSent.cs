using System;
using Swaksoft.Core;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class MessageSent
    {
        public int UserProfileId { get; set; }

        public DateTime DateSent { get; set; }
        
        public string Text { get; set; }
    }
}
