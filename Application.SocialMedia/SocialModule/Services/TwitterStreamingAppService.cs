using System;
using System.IO;
using System.Threading.Tasks;
using Swaksoft.Application.Seedwork.Services;
using Swaksoft.Application.SocialMedia.Resources;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public class TwitterStreamingAppService : AppServiceBase<TwitterStreamingAppService>, IRealTimeStreamingAppService
    {
        private readonly IRealTimeStreamingFactory streamingFactory;
        private readonly IStreamingAdapter streamingAdapter;

        public TwitterStreamingAppService(IRealTimeStreamingFactory streamingFactory, IStreamingAdapter streamingAdapter)
        {
            if (streamingFactory == null) throw new ArgumentNullException("streamingFactory");
            if (streamingAdapter == null) throw new ArgumentNullException("streamingAdapter");

            this.streamingFactory = streamingFactory;
            this.streamingAdapter = streamingAdapter;
        }

        public async Task SubscribeForStreaming(Dto.OAuthRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");

            var options = streamingFactory.GetStreamingOptionsFor<TwitterUserProfile>(request);
            if (options == null)
            {
                var error = string.Format(Messages.api_MissingUserProfile, Messages.const_Twitter, request.UserId);
                throw new InvalidDataException(error);
            }
            
            await streamingAdapter.SubscribeForStreaming(options);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing) return;

            streamingFactory.Dispose();
        }
    }
}
