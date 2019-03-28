using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialMedia.Host.Models.Account;
using Swaksoft.Infrastructure.Crosscutting.Authorization;
using Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia;
using Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia.Entities;
using Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia.ValueObjects;

namespace SocialMedia.Host.Models
{
    public abstract class ModelFactory<TUser>
        where TUser : IdentityUser,new()
    {
        private readonly UserManager<TUser> appUserManager;
        private readonly UrlHelper urlHelper;

        protected ModelFactory(HttpRequestMessage request, UserManager<TUser> appUserManager)
        {
            this.appUserManager = appUserManager;
            urlHelper = new UrlHelper(request);
        }

        protected T Create<T>(TUser appUser)
            where T:UserModel,new()
        {
            return new T
            {
                Url = urlHelper.Link("DefaultApi", new { controller = "Account", action = "GetUserById", id = appUser.Id }),
                Id = appUser.Id,
                UserName = appUser.UserName,
                Email = appUser.Email,
                EmailConfirmed = appUser.EmailConfirmed,
                Roles = appUserManager.GetRolesAsync(appUser.Id).Result,
                Claims = appUserManager.GetClaimsAsync(appUser.Id).Result
            };
        }

        public TUser Create<TModel>(TModel model)
            where TModel : CreateUserModel,new()
        {
            return new TUser
            {
                UserName = string.IsNullOrEmpty(model.UserName) ? model.Email : model.UserName,
                Email = model.Email
            };
        }
    }

    public class ModelFactory : ModelFactory<ApplicationUser>
    {
        public ModelFactory(HttpRequestMessage request, UserManager<ApplicationUser> appUserManager) 
            : base(request, appUserManager)
        {
        }

        public ApplicationUserModel Create(ApplicationUser appUser)
        {
            var viewModel = Create<ApplicationUserModel>(appUser);
            viewModel.FullName = $"{appUser.Name.FirstName} {appUser.Name.LastName}";
            return viewModel;
        }
        
        public ApplicationUser Create(CreateExternalApplicationUserModel model)
        {
            var user = base.Create(model);

            user.Name = new Name(model.FirstName, model.LastName);
            user.SkypeId = model.SkypeId;
            user.Hometown = model.Hometown;
            return user;
        }
    }

}