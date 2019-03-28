using System.Web;
using System.Web.Http;
using Swaksoft.Application.Seedwork.Logging;
using Swaksoft.Application.Seedwork.TypeMapping;
using Swaksoft.Application.Seedwork.Validation;
using Swaksoft.Infrastructure.Crosscutting.Logging;
using Swaksoft.Infrastructure.Crosscutting.TypeMapping;
using Swaksoft.Infrastructure.Crosscutting.Validation;
using log4net.Config;

namespace Swaksoft.Communicator.Host
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(IocContainerConfig.Register);

            XmlConfigurator.Configure();
            RegisterSingletons();
        }

        private static void RegisterSingletons()
        {
            TypeAdapterLocator.SetCurrent(new AutoMapperTypeAdapterFactory());
            EntityValidatorLocator.SetCurrent(new DataAnnotationsEntityValidatorFactory());
            LoggerLocator.SetCurrent(new Log4NetLoggerFactory());
        }
    }
}
