using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Facebook;

namespace SocialMedia.Host.Authentication.Providers
{
    public class FacebookAuthProvider : FacebookAuthenticationProvider
    {
        public FacebookAuthProvider()
        {
        }

        public override Task Authenticated(FacebookAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
            return Task.FromResult<object>(null);
        }
    }
}