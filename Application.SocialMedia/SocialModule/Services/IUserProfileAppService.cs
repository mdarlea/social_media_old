using System;
using Swaksoft.Core;
using Swaksoft.Core.Dto;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public interface IUserProfileAppService : IDisposable
    {
        ActionResult RemoveUserProfile(Dto.UserProfileRequest request);

        ActionResult AddNewSentTweet(Dto.TweetSent request);
    }
}