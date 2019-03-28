using System;
using System.Web.Mvc;
using SocialMedia.Host.Authorization.Filters;


namespace SocialMedia.Host.Controllers
{
    [SocialAuthorize]
    public class HomeController : Controller
    {
        // GET: Home
        [AllowAnonymous]
        public ActionResult Index()
        {            
            return View();
        }       
    }

   
}