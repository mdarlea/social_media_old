using System;
using Swaksoft.Core;
using Swaksoft.Core.Dto;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Contracts
{
    public class TweetResult : ActionResult
    {
        public long Id { get; set; }
        public long? InReplyToStatusId { get; set; }
        public long? InReplyToUserId { get; set; }
        public string InReplyToUserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Text { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
    }
}
