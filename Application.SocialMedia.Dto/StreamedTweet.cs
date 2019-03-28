using System;
using Swaksoft.Core;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class StreamedTweet
    {
        public int Id { get; set; }

        public long StatusId { get; set; }

        public long UserId { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string ProfileImageUrl { get; set; }

        public string Query { get; set; }
    }
}
