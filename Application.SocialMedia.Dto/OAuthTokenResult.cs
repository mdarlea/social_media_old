using Swaksoft.Core;
using Swaksoft.Core.Dto;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class OAuthTokenResult : ActionResult
    {
        public string AccessToken { get; set; }

        public string AccessTokenSecret { get; set; }

        public string ProviderKey { get; set; }
    }
}
