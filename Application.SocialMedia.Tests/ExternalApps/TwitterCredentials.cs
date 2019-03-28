using Swaksoft.Core;
using Swaksoft.Core.External;

namespace Application.SocialMedia.Tests.ExternalApps
{
    public class TwitterCredentials : ExternalProviderCredentials
    {
        public TwitterCredentials()
            : base(ExternalProvider.Twitter,
                   consumerKey: "7N6hnNVhCZQxOTex4ylQVZyfG",
                   consumerSecret: "t579rPenNwRvP9OT4OsDE8lpg1CxaTO4BUzJBwunYRwnkDCAQl")
        {
        }
    }
}
