using Swaksoft.Core;
using Swaksoft.Core.Dto;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class StreamedTweetOptions : SearchOptions
    {
        public string UserId { get; set; }
        public string Query { get; set; }
        public int? MinIdentity { get; set; }
        public int PageSize { get; set; }
    }
}
