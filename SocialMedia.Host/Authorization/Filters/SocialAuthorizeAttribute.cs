using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SocialMedia.Host.Authorization.Filters
{
    public class SocialAuthorizeAttribute: System.Web.Mvc.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //filterContext.Result = new HttpUnauthorizedResult();
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new
                    {
                        route = "Default"
                    }));
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new
                    {
                        route = "Default"
                    }));
            }
        }
    }
}