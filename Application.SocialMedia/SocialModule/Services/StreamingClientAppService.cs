using System;
using Swaksoft.Application.Seedwork.Services;
using Swaksoft.Application.SocialMedia.Dto;
using Swaksoft.Core.Dto;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts.Streaming;
using Swaksoft.Infrastructure.Crosscutting.Extensions;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public class StreamingClientAppService : AppServiceBase<StreamingClientAppService>, IStreamingClientAppService
    {
        private readonly IClientsRegistry _clientsRegistry;
        private readonly IUserProfileRepository _userProfileRepository;

        public StreamingClientAppService(IClientsRegistry clientsRegistry, IUserProfileRepository userProfileRepository)
        {
            if (clientsRegistry == null) throw new ArgumentNullException("clientsRegistry");
            if (userProfileRepository == null) throw new ArgumentNullException("userProfileRepository");
            _clientsRegistry = clientsRegistry;
            _userProfileRepository = userProfileRepository;
        }

        public StreamingClientResult GetActiveClients()
        {
                var clients = _clientsRegistry.GetActiveClients().ProjectedAsCollection<StreamingClient>();

                return new StreamingClientResult
                {
                    Status = ActionResultCode.Success,
                    Items = clients
                };
        }
        

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing) return;

            if (_userProfileRepository != null)
            {
                _userProfileRepository.Dispose();
            }
        }
    }
}
