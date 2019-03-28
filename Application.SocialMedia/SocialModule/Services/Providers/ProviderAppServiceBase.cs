using System;
using System.Linq;
using Swaksoft.Application.Seedwork.Extensions;
using Swaksoft.Application.Seedwork.Services;
using Swaksoft.Core;
using Swaksoft.Core.Dto;
using Swaksoft.Domain.Seedwork.Events;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts;
using Swaksoft.Domain.SocialMedia.SocialModule.Events;
using Swaksoft.Infrastructure.Crosscutting.Extensions;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services.Providers
{
    public abstract class ProviderAppServiceBase<T,TProfile> : AppServiceBase<T>
        where TProfile:UserProfile
    {
        private readonly IOAuthAuthorizationAdapter _ioAuthAuthorizationAdapter;
        private readonly IUserProfileAdapter _userProfileAdapter;
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        protected ProviderAppServiceBase(
            IOAuthAuthorizationAdapter ioAuthAuthorizationAdapter,
            IUserProfileAdapter userProfileAdapter,
            IUserRepository userRepository,
            IUserProfileRepository userProfileRepository)
        {
            if (ioAuthAuthorizationAdapter == null) throw new ArgumentNullException("ioAuthAuthorizationAdapter");
            if (userProfileAdapter == null) throw new ArgumentNullException("userProfileAdapter");
            if (userRepository == null) throw new ArgumentNullException("userRepository");
            if (userProfileRepository == null) throw new ArgumentNullException("userProfileRepository");
            _ioAuthAuthorizationAdapter = ioAuthAuthorizationAdapter;
            _userProfileAdapter = userProfileAdapter;
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
        }

       public Dto.UserProfileResult Connect(Dto.OAuthRequest request)
       {
            if (request == null) throw new ArgumentNullException("request");

                var result = new Dto.UserProfileResult
                {
                    Status = ActionResultCode.Errored
                };

                var accessTokenResult = _ioAuthAuthorizationAdapter.GetAccessToken(new OAuthOptions
                {
                    ClientCredentials = request.ClientCredentials,
                    OAuthToken = request.OAuthToken,
                    OAuthVerifier = request.OAuthVerifier,
                    CallbackUrl = request.CallbackUrl
                });

                if (accessTokenResult == null || accessTokenResult.Status != ActionResultCode.Success)
                {
                    result.Message = @"Could not get information about the current user";
                    return result;
                }

                //authorize the user and gets the profile from the vendor
                var userProfileResult = _userProfileAdapter.GetUserProfile(
                    request.ClientCredentials, 
                    accessTokenResult.AccessToken, 
                    accessTokenResult.AccessTokenSecret);

                if (userProfileResult == null || userProfileResult.Status != ActionResultCode.Success)
                {
                   result.Message = @"Could not get information about the current user";
                   return result;
                }

                GetLog().LogInfo(@"User {0} was successfully authorized by Twitter", userProfileResult.UserName);

                //check if a profile for this user exists
                var profile = _userProfileRepository.GetByUserId<TProfile>(request.UserId).SingleOrDefault();
            
                if (profile == null)
                {
                    //get the user
                    var user = _userRepository.GetSingle(u => u.Id == request.UserId);
                    if (user == null)
                    {
                        result.Message = @"Could not get information about the current user";
                        return result;
                    }

                    //creates the user profile
                    profile = CreateUserProfile(accessTokenResult, userProfileResult, user);
                    _userProfileRepository.SaveEntity(profile);

                    GetLog().Debug(@"User {0}: {1} was successfully added to the system", profile.Id, profile.UserName);

                    //raise event
                    if (!string.IsNullOrWhiteSpace(request.SubscriptionMessage))
                    {
                        DomainEvents.Raise(new UserProfileCreated(
                            request.ClientCredentials,
                            profile.Id,
                            profile.AuthorizationToken,
                            request.SubscriptionMessage));
                    }    
                }

                //set the user image
                var dto = profile.ProjectedAs<Dto.UserProfileResult>();
                dto.ProfilePictureUrl = userProfileResult.ProfilePictureUrl;

                return dto;
        }

        protected abstract TProfile CreateUserProfile(AccessTokenResult accessTokenResult, UserProfileResult userProfileResult, User user);

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_userRepository != null)
            {
                _userRepository.Dispose();
            }
            if (_userProfileRepository != null)
            {
                _userProfileRepository.Dispose();
            }
        }
    }
}
