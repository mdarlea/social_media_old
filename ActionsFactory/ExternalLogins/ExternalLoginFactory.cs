using System;
using System.Security.Claims;

namespace SocialMedia.DI.Registry.ActionsFactory.ExternalLogins
{
    public class ExternalLoginFactory<T> : IExternalLoginFactory
        where T:ExternalLoginData, new()
    {
        public ExternalLoginData Create(ClaimsIdentity identity)
        {
            return FromIdentity(identity);
        }

        public virtual T FromIdentity(ClaimsIdentity identity)
        {
            return FromIdentity(identity, data => { });
        }

        protected T FromIdentity(ClaimsIdentity identity, Action<T> callback)
        {
            var providerKeyClaim = identity?.FindFirst(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(providerKeyClaim?.Issuer) || string.IsNullOrEmpty(providerKeyClaim.Value))
            {
                return null;
            }

            if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
            {
                return null;
            }

            var loginData = new T
            {
                LoginProvider = providerKeyClaim.Issuer,
                ProviderKey = providerKeyClaim.Value,
                UserName = identity.FindFirst(ClaimTypes.Name)?.Value,
                ExternalAccessToken = identity.FindFirst("ExternalAccessToken")?.Value,
            };

            callback?.Invoke(loginData);

            return loginData;
        }
    }
}