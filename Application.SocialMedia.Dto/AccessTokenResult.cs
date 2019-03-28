using Swaksoft.Core;
using Swaksoft.Core.Dto;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class AccessTokenResult : ActionResult
    {
        public string UserName { get; set; }

        public string ExternalUserId { get; set; }

        public string AccessToken { get; set; }
    }
}
