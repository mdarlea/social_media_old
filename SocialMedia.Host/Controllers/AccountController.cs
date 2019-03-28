using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using SocialMedia.Host.Authentication;
using SocialMedia.Host.Models;
using SocialMedia.Host.Models.Account;
using Swaksoft.Application.SocialMedia.SocialModule.Services;
using Swaksoft.Core.Dto;
using Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia;
using Swaksoft.Infrastructure.Crosscutting.Authorization.SocialMedia.Entities;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Token;
using Dto = Swaksoft.Application.SocialMedia.Dto;
using System.Threading;

namespace SocialMedia.Host.Controllers
{
    [System.Web.Http.RoutePrefix("api/Account")]
    [Authorize]
    public class AccountController : ApiControllerBase
    {
        private readonly Swaksoft.Infrastructure.Crosscutting.Authorization.UserManager<ApplicationUser> userManager;
        private readonly ApplicationSignInManager signInManager;
        private readonly IAuthenticationManager authenticationManager;
        private readonly IAccessTokenGenerator<ApplicationUser> accessTokenGenerator;
        private readonly IAddressAppService addressAppService;

        public AccountController(Swaksoft.Infrastructure.Crosscutting.Authorization.UserManager<ApplicationUser> userManager, 
            ApplicationSignInManager signInManager,
            IAuthenticationManager authenticationManager,
            IAccessTokenGenerator<ApplicationUser> accessTokenGenerator,
            IAddressAppService addressAppService)
        {
            if (userManager == null) throw new ArgumentNullException(nameof(userManager));
            if (signInManager == null) throw new ArgumentNullException(nameof(signInManager));
            if (authenticationManager == null) throw new ArgumentNullException(nameof(authenticationManager));
            if (accessTokenGenerator == null) throw new ArgumentNullException(nameof(accessTokenGenerator));
            if (addressAppService == null) throw new ArgumentNullException(nameof(addressAppService));
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authenticationManager = authenticationManager;
            this.accessTokenGenerator = accessTokenGenerator;
            this.addressAppService = addressAppService;
        }

        private ModelFactory modelFactory;
        private ModelFactory ModelFactory 
            => modelFactory 
            ?? (modelFactory = new ModelFactory(Request, userManager));

        // POST api/Account/Register
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(CreateApplicationUserModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //creates the user
            var user = ModelFactory.Create(model);
            var result = await userManager.CreateAsync(user, model.Password);
            var errorResult = GetErrorResult(result);
            if (errorResult != null)
            {
                return errorResult;
            }
            if (!result.Succeeded) return BadRequest(ModelState);

            //creates the address
            var address = await CreateAddress(user.Id, model);
            if (address.Status != ActionResultCode.Success)
            {
                return GetErrorResult(address);
            }
            
            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            // Send an email with this link
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var url = Url.Link("Default", new { controller = "Login", action = "ConfirmEmail", userId=user.Id, code  });
            var callbackUrl = new Uri(url);
            await userManager.SendEmailAsync(
                user.Id,
                "Confirm your account",
                "Please confirm your account by clicking " + callbackUrl);
            return Ok();
        }

        private async Task<Dto.AddressResult> CreateAddress(string userId, CreateExternalApplicationUserModel model)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException(nameof(userId));
            if (model == null) throw new ArgumentNullException(nameof(model));

            var address = new Dto.Address
            {
                StreetAddress = model.Address.StreetAddress,
                SuiteNumber = model.Address.SuiteNumber,
                City = model.Address.City,
                State = model.Address.State,
                Zip = model.Address.Zip,
                CountryIsoCode = model.Address.CountryIsoCode,
                GeolocationStreet = model.Address.GeolocationStreet,
                GeolocationStreetNumber = model.Address.GeolocationStreetNumber,
                Latitude = model.Address.Latitude,
                Longitude = model.Address.Longitude,
                IsMainAddress = true
            };
            return await addressAppService.AddAddressToUserAsync(new Dto.AddAddressToUserRequest()
            {
                UserId = userId,
                Address = address
            });
        }

    
    [AllowAnonymous]
    [HttpGet]
    [Route("LoginExternal")]
    public IHttpActionResult LoginExternal(string provider, string callbackUrl)
    {
      //Request a redirect to the external login provider      
      var uri = new Uri("http://localhost:52499/api/account/externallogincallback/SocialMediaApp");
      return new ChallengeResult(Request, provider, new Uri(callbackUrl));
    }

        [AllowAnonymous]
        [HttpGet]
        [Route("ExternalLoginCallback/{clientId}")]
        public async Task<IHttpActionResult> ExternalLoginCallback(string clientId)
        {
            var loginInfo = await authenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return BadRequest("Could not get the external information");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await signInManager.ExternalSignInAsync(loginInfo, false);

            switch (result)
            {
                case SignInStatus.Success:
                    // obstains the local access token
                    var user = await userManager.FindAsync(new UserLoginInfo(loginInfo.Login?.LoginProvider, loginInfo.Login?.ProviderKey));
                    return await AuthorizedUser(clientId, user);
                 case SignInStatus.Failure:
                    var userInfo = new ExternalLoginModel
                    {
                        Name = loginInfo.ExternalIdentity?.Name,
                        Provider = loginInfo.Login?.LoginProvider
                    };
                    return Ok(userInfo);
            }

            return Ok();

        }
        // POST api/Account/RegisterExternal
        [HttpPost]
        [AllowAnonymous]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(CreateExternalApplicationUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.Identity.IsAuthenticated)
            {
                return BadRequest("User is already authorized");
            }

            var externalLoginInfo = await authenticationManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null)
            {
                return BadRequest("Invalid Provider or External Access Token");
            }

            var providerKey = externalLoginInfo.Login.ProviderKey;
            
            var user = await userManager.FindByIdAsync(providerKey);

            var hasRegistered = user != null;

            if (hasRegistered)
            {
                return BadRequest("External user is already registered");
            }

            user = ModelFactory.Create(model);
            var result = await userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            var address = await CreateAddress(user.Id, model);
            if (address.Status != ActionResultCode.Success)
            {
                return GetErrorResult(address);
            }
            
            var info = new ExternalLoginInfo
            {
                DefaultUserName = model.UserName,
                Login = new UserLoginInfo(model.Provider, providerKey)
            };

            result = await userManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return await AuthorizedUser(model.ClientId, user);
        }

        private async Task<IHttpActionResult> AuthorizedUser(string clientId, ApplicationUser user)
        {
            //generate access token response
            var accessTokenResponse =
                await
                    accessTokenGenerator.GenerateLocalAccessToken(user, clientId, Startup.GetAccessTokenExpireTimeSpan());
            DateTime expires;
            DateTime.TryParse(accessTokenResponse.Expires, out expires);

            return Ok(new AuthUserModel
            {
                UserName = user.UserName,
                Name = user.Name?.FirstName,
                Token = accessTokenResponse.Token,
                UserId = user.Id,
                Expires = expires
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IHttpActionResult> Login(LoginViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
            
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            //signs in the user
            var result = await signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password,true,false);

            switch (result)
            {
                case SignInStatus.Success:
                    var user = await userManager.FindByNameAsync(viewModel.UserName);

                    var hasRegistered = user != null;

                    if (!hasRegistered)
                    {
                        return BadRequest("User is not registered");
                    }

                    var emailConfirmed = await userManager.IsEmailConfirmedAsync(user.Id);
                    if (!emailConfirmed)
                    {
                        return BadRequest("You must confirm your email.");
                    }

                    return await AuthorizedUser(viewModel.ClientId,user);
                case SignInStatus.LockedOut:
                    
                case SignInStatus.RequiresVerification:
                   
                case SignInStatus.Failure:
                    return BadRequest("Invalid user name or password");
                default:
                    return Ok(result);
            }
        }

        [System.Web.Http.Authorize(Roles = "Admin")]
        [System.Web.Http.Route("User/{id}")]
        public async Task<IHttpActionResult> GetUserById(string id)
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                return Ok(ModelFactory.Create(user));
            }

            return NotFound();

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                addressAppService.Dispose();
            }
        }
    }

  internal class ChallengeResult : IHttpActionResult
  {
    public ChallengeResult(      
      HttpRequestMessage request,
      string provider,
      Uri redirectUri)
    {       
      MessageRequest = request;
      LoginProvider = provider;
      RedirectUri = redirectUri;
    }

    public string LoginProvider { get; }
    public Uri RedirectUri { get; }
    public HttpRequestMessage MessageRequest { get; }

    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    {
      var properties = new AuthenticationProperties { RedirectUri = RedirectUri.ToString() };

       MessageRequest.GetOwinContext().Authentication.Challenge(properties, LoginProvider);

      var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
      response.RequestMessage = MessageRequest;

      return Task.FromResult(response);
    }
  }
}