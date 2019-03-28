using Swaksoft.Domain.Seedwork;

namespace SocialMedia.DI.Registry.ActionsFactory.ExternalLogins
{
    public class ExternalLoginActionsFactory : ActionsFactory<IExternalLoginFactory>
    {
        public ExternalLoginActionsFactory()
        {
            Register("twitter", () => new TwitterLoginFactory());
            Register("facebook", () => new ExternalLoginFactory<ExternalLoginData>());
            Register("google", () => new ExternalLoginFactory<ExternalLoginData>());
        }
    }
}