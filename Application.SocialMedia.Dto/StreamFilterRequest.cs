using System;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class StreamFilterRequest
    {
        public string UserId { get; set; }

        public int? Id { get; set; }
        public string Query { get; set; }
    }
}
