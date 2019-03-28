using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace SocialMedia.Host.Authentication.Providers
{
    public class QueryStringEnabledOAuthBearerAuthenticationProvider : OAuthBearerAuthenticationProvider
    {
        private readonly string name;

        public QueryStringEnabledOAuthBearerAuthenticationProvider()
            : this(OAuthDefaults.AuthenticationType)
        {
        }

        public QueryStringEnabledOAuthBearerAuthenticationProvider(string name)
        {
            this.name = name;
        }

        public override async Task RequestToken(OAuthRequestTokenContext context)
        {
            // try to read token from base class (header) if possible
            await base.RequestToken(context);
            if (string.IsNullOrWhiteSpace(context.Token))
            {
                // try to read token from query string
                var token = context.Request.Query.Get(name);
                if (!string.IsNullOrWhiteSpace(token))
                {
                    context.Token = token;
                }
            }
        }
    }
}