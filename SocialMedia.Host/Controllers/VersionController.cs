using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Host.Controllers
{
    public class VersionController : Controller
    {
        // GET: Version
        public ActionResult Index()
        {
            return View();
        }
    }
}