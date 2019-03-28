using System;
using Swaksoft.Core;
using Swaksoft.Core.Dto;

namespace Swaksoft.Application.SocialMedia.Dto
{
    public class OAuthUrlResult : ActionResult
    {
        public Uri AuthorizationUri { get; set; }
        public Uri AuthenticationUri { get; set; }
    }
}
