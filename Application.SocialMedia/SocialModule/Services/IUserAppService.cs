using System;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public interface IUserAppService : IDisposable
    {
        Dto.CollectionActionResult<Dto.UserProfile> GetUserProfiles(Dto.UserProfileRequest request);

        Dto.CollectionActionResult<Dto.Message> GetUserMessages(Dto.MessageRequest request);
        Dto.CollectionActionResult<Dto.StreamFilter> GetStreamFilters(Dto.StreamFilterRequest request);

        Dto.MessageResult AddNewUserMessage(Dto.MessageRequest request);
        Dto.StreamFilterResult AddNewStreamFilter(Dto.StreamFilterRequest request);

        Dto.MessageOperationResult AssociateMessageToStreamFilter(Dto.MessageOperationRequest request);
    }
}