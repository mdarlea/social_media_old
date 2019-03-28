using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using log4net.Config;
using Swaksoft.Application.Seedwork.Logging;
using Swaksoft.Application.Seedwork.TypeMapping;
using Swaksoft.Application.Seedwork.Validation;
using Swaksoft.Infrastructure.Crosscutting.Logging;
using Swaksoft.Infrastructure.Crosscutting.TypeMapping;
using Swaksoft.Infrastructure.Crosscutting.Validation;

namespace SocialMedia.Host
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(IocContainerConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            //log4net configuration
            XmlConfigurator.Configure();

            RegisterSingletons();
        }

        private static void RegisterSingletons()
        {
            TypeAdapterLocator.SetCurrent(new AutoMapperTypeAdapterFactory());
            EntityValidatorLocator.SetCurrent(new DataAnnotationsEntityValidatorFactory());
            LoggerLocator.SetCurrent(new Log4NetLoggerFactory());
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}