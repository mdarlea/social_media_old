using Swaksoft.Core;
using Swaksoft.Core.Dto;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Contracts
{
    public class AccessTokenResult : ActionResult
    {
        public string UserName { get; set; }

        public string ExternalUserId { get; set; }

        public string AccessToken { get; set; }

        public string AccessTokenSecret { get; set; }
    }
}
