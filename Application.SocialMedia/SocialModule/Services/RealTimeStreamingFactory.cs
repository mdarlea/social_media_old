using System;
using System.IO;
using System.Linq;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamFilterAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public class RealTimeStreamingFactory : IRealTimeStreamingFactory
    {
        private readonly IUserRepository userRepository;
        private readonly IStreamFilterRepository streamFilterRepository;

        public RealTimeStreamingFactory( IUserRepository userRepository,IStreamFilterRepository streamFilterRepository)
        {
            if (userRepository == null) throw new ArgumentNullException("userRepository");
            if (streamFilterRepository == null) throw new ArgumentNullException("streamFilterRepository");

            this.userRepository = userRepository;
            this.streamFilterRepository = streamFilterRepository;
        }

        public StreamingOptions GetStreamingOptionsFor<TProfile>(Dto.OAuthRequest request)
            where TProfile : UserProfile
        {
            if (request == null) throw new ArgumentNullException("request");
            if (string.IsNullOrWhiteSpace(request.UserId)) throw new ArgumentException(@"UserId was not set", "request");

            var user = userRepository.GetUserWithUserProfiles(request.UserId);
            if (user == null)
            {
                throw new InvalidDataException(@"Could not get information about the current user");
            }

            var userProfile = user.UserProfiles.OfType<TProfile>().FirstOrDefault();
            if (userProfile == null) return null;

            var specification = StreamFilterSpecifications.StreamFiltersByUserId(request.UserId);
            var streamFilters = streamFilterRepository.AllMatching(specification);

            return new StreamingOptions
            {
                ClientCredentials = request.ClientCredentials,
                ClientSettings = new ClientSettings
                {
                    ClientName = request.ClientCredentials.Type.ToString()
                },
                ExternalUserOptions = new ExternalUserOptions
                {
                    AuthorizationToken = userProfile.AuthorizationToken,
                    UserProfileId = userProfile.Id,
                    UserName = userProfile.UserName
                },
                Queries = streamFilters.Select(filter => filter.Query).ToList()
            };
        }

        #region dispose
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            userRepository.Dispose();
            streamFilterRepository.Dispose();
        }
        #endregion dispose
    }
}
