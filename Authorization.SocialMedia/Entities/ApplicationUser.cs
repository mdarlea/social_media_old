using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia.ValueObjects;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public Name Name { get; set; }

        public string Hometown { get; set; }
       
        [MaxLength(100)]
        public string FacebookId { get; set; }

        [MaxLength(100)]
        public string SkypeId { get; set; }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(Swaksoft.Infrastructure.Crosscutting.Authorization.UserManager<ApplicationUser> manager)
        {
            return await GenerateUserIdentityAsync(manager, DefaultAuthenticationTypes.ExternalCookie);
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(Swaksoft.Infrastructure.Crosscutting.Authorization.UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}