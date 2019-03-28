using System.Security.Claims;

namespace SocialMedia.DI.Registry.ActionsFactory.ExternalLogins { 
    public interface IExternalLoginFactory
    {
        ExternalLoginData Create(ClaimsIdentity identity);
    }
}
