using System;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public interface IStreamedTweetAppService : IDisposable
    {
        Dto.PagedListActionResult FindStreamedTweets(Dto.StreamedTweetOptions request);
    }
}