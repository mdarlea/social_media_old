using Swaksoft.Core;
using Swaksoft.Core.Dto;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Contracts
{
    public class UserProfileResult : ActionResult
    {
        public string UserName { get; set; }

        public string ExternalUserId { get; set; }

        public string Name { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
