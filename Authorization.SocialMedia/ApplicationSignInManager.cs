using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia.Entities;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia
{
    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationSignInManager(
            UserManager<ApplicationUser> userManager, 
            IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
            if (userManager == null) throw new ArgumentNullException(nameof(userManager));
            this.userManager = userManager;
            AuthenticationType = OAuthDefaults.AuthenticationType;
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync(userManager);
        }
    }
}