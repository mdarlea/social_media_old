using System.Security.Claims;

namespace SocialMedia.DI.Registry.ActionsFactory.ExternalLogins
{
    public class TwitterLoginFactory : ExternalLoginFactory<TwitterLoginData>
    {
        public const string ExternalAccessToken = "ExternalAccessToken";
        public const string ScreenName = "ScreenName";
        public const string UserId = "UserId";
        public const string AccessTokenSecret = "AccessTokenSecret";

        public override TwitterLoginData FromIdentity(ClaimsIdentity identity)
        {
            return base.FromIdentity(identity, data =>
            {
                data.ScreenName = identity.FindFirst(ScreenName)?.Value;
                data.UserId = identity.FindFirst(UserId)?.Value;
                data.ExternalAccessTokenSecret = identity.FindFirst(AccessTokenSecret)?.Value;
            });
        }
    }
}