using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SocialMedia.Host.Authentication;
using Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia;
using Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia.Entities;

namespace SocialMedia.Host.Controllers
{
    public class LoginController : Controller
    {
        private readonly Swaksoft.Infrastructure.Crosscutting.Authorization.UserManager<ApplicationUser> userManager;

        public LoginController(Swaksoft.Infrastructure.Crosscutting.Authorization.UserManager<ApplicationUser> userManager)
        {
            if (userManager == null) throw new ArgumentNullException(nameof(userManager));
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        
        [AllowAnonymous]
        public ActionResult ConfirmEmail(string userId = "", string code = "")
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", "User Id and Code are required");
            }

            var result = userManager.ConfirmEmail(userId, code);
            return View(result);
        }
    }
    
}
