namespace SocialMedia.DI.Registry
{
    public interface IMvcDependencyResolverFactory
    {
        System.Web.Mvc.IDependencyResolver CreateResolver();
    }

    public interface IApiDependencyResolverFactory
    {
        System.Web.Http.Dependencies.IDependencyResolver CreateResolver();
    }
}
