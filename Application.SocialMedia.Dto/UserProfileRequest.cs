using Swaksoft.Core;
using Swaksoft.Core.External;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class UserProfileRequest
    {
        public ExternalProvider Type { get; set; }

        public string UserId { get; set; }
    }
}
