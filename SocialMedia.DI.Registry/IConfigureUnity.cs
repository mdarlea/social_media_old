using Microsoft.Practices.Unity;

namespace SocialMedia.DI.Registry
{
    public interface IConfigureUnity
    {
        void Configure(IUnityContainer container);
    }
}
