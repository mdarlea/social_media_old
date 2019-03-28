using System;
using Swaksoft.Application.SocialMedia.Dto;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public interface IStreamingClientAppService : IDisposable
    {
        StreamingClientResult GetActiveClients();
    }
}