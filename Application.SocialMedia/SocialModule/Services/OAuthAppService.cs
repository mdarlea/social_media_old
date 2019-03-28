using System;
using Swaksoft.Application.Seedwork.Services;
using Swaksoft.Core;
using Swaksoft.Core.Dto;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts;
using Swaksoft.Infrastructure.Crosscutting.Extensions;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public class OAuthAppService : AppServiceBase<OAuthAppService>, IOAuthAppService
    {
        private readonly IOAuthAuthorizationAdapter _ioAuthAuthorizationAdapter;
        
        public OAuthAppService(IOAuthAuthorizationAdapter ioAuthAuthorizationAdapter)
        {
            if (ioAuthAuthorizationAdapter == null) throw new ArgumentNullException("ioAuthAuthorizationAdapter");
            
            _ioAuthAuthorizationAdapter = ioAuthAuthorizationAdapter;
        }

        public Dto.OAuthUrlResult Authorize(Dto.OAuthRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");

                var uri = _ioAuthAuthorizationAdapter.Authorize(new OAuthOptions
                {
                    ClientCredentials = request.ClientCredentials,
                    CallbackUrl = request.CallbackUrl
                });
                
                //returns success
                return (uri != null) ? new Dto.OAuthUrlResult
                {
                    Status = ActionResultCode.Success,
                    AuthorizationUri = uri
                } : new Dto.OAuthUrlResult
                {
                    Status = ActionResultCode.Errored,
                    Message = @"Could not get the OAuth authorization url"
                };
        }

        public Dto.OAuthUrlResult Authenticate(Dto.OAuthRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");

                var uri = _ioAuthAuthorizationAdapter.Authenticate(new OAuthOptions
                {
                     ClientCredentials = request.ClientCredentials,
                     CallbackUrl = request.CallbackUrl
                });

                //returns success
                return (uri != null) ? new Dto.OAuthUrlResult
                {
                    Status = ActionResultCode.Success,
                    AuthenticationUri = uri
                } : new Dto.OAuthUrlResult
                {
                    Status = ActionResultCode.Errored,
                    Message = @"Could not get the OAuth authorization url"
                };
        }

        public Dto.AccessTokenResult GetAccessToken(Dto.OAuthRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");

                var result = new Dto.AccessTokenResult
                {
                    Status = ActionResultCode.Errored
                };

                //authorize the user and gets the profile from the vendor
                var accessTokenResult = _ioAuthAuthorizationAdapter.GetAccessToken(new OAuthOptions
                {
                    ClientCredentials = request.ClientCredentials,
                    OAuthToken = request.OAuthToken,
                    OAuthVerifier = request.OAuthVerifier,
                    CallbackUrl = request.CallbackUrl});

                if (accessTokenResult == null)
                {
                    result.Message = @"Could not get information about the current user";
                    return result;
                }
                GetLog().Debug(@"User {0} was successfully authorized", accessTokenResult.ExternalUserId);

                return accessTokenResult.ProjectedAs<Dto.AccessTokenResult>();
        }
    }
}
