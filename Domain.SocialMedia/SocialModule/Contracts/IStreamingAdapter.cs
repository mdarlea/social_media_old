using System;
using System.Threading.Tasks;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Contracts
{
    public interface IStreamingAdapter
    {
        Task SubscribeForStreaming(StreamingOptions streamingOptions);
    }
}