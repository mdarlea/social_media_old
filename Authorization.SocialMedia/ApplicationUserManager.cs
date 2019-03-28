using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia.Entities;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly IIdentityMessageService emailService;
        private readonly IDataProtectionProvider dataProtectionProvider;

        public ApplicationUserManager(
            IUserStore<ApplicationUser> store, 
            IIdentityMessageService emailService, IDataProtectionProvider dataProtectionProvider) 
            : base(store)
        {
            if (store == null) throw new ArgumentNullException(nameof(store));
            this.emailService = emailService;
            this.dataProtectionProvider = dataProtectionProvider;
            Create();
        }

        private void Create()
        {
            // Configure validation logic for usernames
            UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            EmailService = emailService;

            //var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(6)
                };
            }
        }

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            try
            {
                return await base.CreateAsync(user);
            }
            catch (DbEntityValidationException ex)
            {
                var errors = (from e in ex.EntityValidationErrors
                              from er in e.ValidationErrors
                              select er.ErrorMessage);

                return new IdentityResult(errors);
            }
        }
    }
}