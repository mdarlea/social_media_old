using System;
using System.Collections.Generic;
using System.Linq;
using Swaksoft.Application.Seedwork.Extensions;
using Swaksoft.Application.Seedwork.Services;
using Swaksoft.Core;
using Swaksoft.Core.Dto;
using Swaksoft.Core.External;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamFilterAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts;
using Swaksoft.Infrastructure.Crosscutting.Extensions;

namespace Swaksoft.Application.SocialMedia.SocialModule.Services
{
    public class UserAppService: AppServiceBase<UserAppService>, IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStreamFilterRepository _streamFilterRepository;
        private readonly IEnumerable<ExternalProviderCredentials> _externalProviderCredentials;
        private readonly IEnumerable<IUserProfileAdapter> _userProfileServiceAgents;
        
        public UserAppService(
            IUserRepository userRepository, 
            IStreamFilterRepository streamFilterRepository,
            IEnumerable<ExternalProviderCredentials> externalProviderCredentials,
            IEnumerable<IUserProfileAdapter> userProfileServiceAgents)
        {
            if (userRepository == null) throw new ArgumentNullException("userRepository");
            if (streamFilterRepository == null) throw new ArgumentNullException("streamFilterRepository");
            if (externalProviderCredentials == null) throw new ArgumentNullException("externalProviderCredentials");
            if (userProfileServiceAgents == null) throw new ArgumentNullException("userProfileServiceAgents");
            
            _userRepository = userRepository;
            _streamFilterRepository = streamFilterRepository;
            _externalProviderCredentials = externalProviderCredentials;
            _userProfileServiceAgents = userProfileServiceAgents;
        }
        
        public Dto.CollectionActionResult<Dto.UserProfile> GetUserProfiles(Dto.UserProfileRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            if (string.IsNullOrWhiteSpace(request.UserId)) throw new ArgumentNullException("request",@"UserId cannot be null");


                var result = new Dto.CollectionActionResult<Dto.UserProfile>
                {
                    Status = ActionResultCode.Errored
                };

               //get the user
                var user = _userRepository.GetUserWithUserProfiles(request.UserId);
                if (user == null)
                {
                    result.Message = @"Could not get information about the current user";
                    return result;
                }

                //returns the user profiles
                var dtos =  LoadUserProfiles(user);

                //returns success
                return new Dto.CollectionActionResult<Dto.UserProfile>
                {
                    Status = ActionResultCode.Success,
                    Items = dtos
                };
        }

        public Dto.CollectionActionResult<Dto.Message> GetUserMessages(Dto.MessageRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");

                var result = new Dto.CollectionActionResult<Dto.Message>
                {
                    Status = ActionResultCode.Errored
                };

                var user = _userRepository.GetUserWithMessages(request.UserId);
                if (user == null)
                {
                    result.Message = @"Could not get information about the current user";
                    return result;
                }

                return new Dto.CollectionActionResult<Dto.Message>
                {
                    Status = ActionResultCode.Success,
                    Items = user.Messages.ProjectedAsCollection<Dto.Message>()
                };
        }

        public Dto.CollectionActionResult<Dto.StreamFilter> GetStreamFilters(Dto.StreamFilterRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");

                var result = new Dto.CollectionActionResult<Dto.StreamFilter>
                {
                    Status = ActionResultCode.Errored
                };

                var user = _userRepository.GetUserWithMessageOperations(request.UserId);
                if (user == null)
                {
                    result.Message = @"Could not get information about the current user";
                    return result;
                }

                var specification = StreamFilterSpecifications.StreamFiltersByUserId(request.UserId);
                var streamFilters = _streamFilterRepository.AllMatching(specification);

                return new Dto.CollectionActionResult<Dto.StreamFilter>
                {
                    Status = ActionResultCode.Success,
                    Items = streamFilters.ProjectedAsCollection<Dto.StreamFilter>()
                };
        }

        public Dto.MessageResult AddNewUserMessage(Dto.MessageRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            if (string.IsNullOrWhiteSpace(request.UserId)) throw new ArgumentNullException("request", @"request.UserId");

                var result = new Dto.MessageResult
                {
                    Status = ActionResultCode.Errored
                };

                using (var transaction = _userRepository.BeginTransaction())
                {
                    var user = _userRepository.GetUserWithMessages(request.UserId);
                    if (user == null)
                    {
                        result.Message = string.Format("Could not get information about the '{0}' user", request.UserId);
                        return result;
                    }

                    //get the stream filter
                    var message = user.AddNewMessage(request.Text);

                    transaction.Commit();

                    return message.ProjectedAs<Dto.MessageResult>();
                }
        }

        public Dto.StreamFilterResult AddNewStreamFilter(Dto.StreamFilterRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            if (string.IsNullOrWhiteSpace(request.UserId)) throw new ArgumentNullException("request", @"request.UserId");

                var result = new Dto.StreamFilterResult
                {
                    Status = ActionResultCode.Errored
                };

                    var user = _userRepository.GetSingle(u => u.Id == request.UserId);
                    if (user == null)
                    {
                        result.Message = string.Format("Could not get information about the '{0}' user", request.UserId);
                        return result;
                    }

                    //check if query exists
                    var streamFilter = user.GetStreamFilterByQuery(request.Query);
                    if (streamFilter != null)
                    {
                        result.Message =  string.Format("A query {0} has already been created in the system for the {1} user", request.Query, user.Id);
                        return result;
                    }
                  
                    streamFilter = StreamFilterFactory.CreateStreamFilter(user, request.Query);
                    return _streamFilterRepository.SaveEntity(streamFilter).ProjectAs<Dto.StreamFilterResult>();
        }
        
        public Dto.MessageOperationResult AssociateMessageToStreamFilter(Dto.MessageOperationRequest request)
        {
            if (request == null) throw new ArgumentNullException("request");
            if (string.IsNullOrWhiteSpace(request.UserId)) throw new ArgumentNullException("request", @"request.UserId");

                var result = new Dto.MessageOperationResult
                {
                    Status = ActionResultCode.Errored
                };

                //get the stream filter
                var streamFilterId = request.StreamFilterId ?? 0;
                var messageId = request.MessageId ?? 0;

                using (var transaction = _userRepository.BeginTransaction())
                {
                    //get the current user
                    var user = _userRepository.GetUserWithMessageOperations(request.UserId);
                    if (user == null)
                    {
                        result.Message = string.Format("Could not get information about the '{0}' user", request.UserId);
                        return result;
                    }

                    StreamFilter streamFilter;
                    if (streamFilterId > 0)
                    {
                        streamFilter = user.GetStreamFilterById(streamFilterId);
                    }
                    else
                    {
                        streamFilter = StreamFilterFactory.CreateStreamFilter(user, request.Query);
                        _streamFilterRepository.SaveEntity(streamFilter);
                    }
                    
                    var messageOperation = messageId > 0
                        ? user.AssociateMessageToStreamFilter(streamFilter, messageId)
                        : user.AssociateMessageToStreamFilter(streamFilter, request.Message);

                    transaction.Commit();

                    return messageOperation.ProjectedAs<Dto.MessageOperationResult>();
                }
        }

        private List<Dto.UserProfile> LoadUserProfiles(User user)
        {
            var userProfiles = user.UserProfiles;
            var dtos = userProfiles.ProjectedAsCollection<Dto.UserProfile>();

            foreach (var userProfile in userProfiles)
            {
                var dto = dtos.FirstOrDefault(d => d.ProviderKey == userProfile.ProviderKey.ToString());
                if (dto == null) continue;

                var service = _userProfileServiceAgents.SingleOrDefault(s => s.Provider == userProfile.ProviderKey);
                if (service == null)
                    throw new InvalidOperationException(
                        string.Format("Missing user profile service agent for the {0} provider", userProfile.ProviderKey));

                var credentials = _externalProviderCredentials.SingleOrDefault(c => c.Type == userProfile.ProviderKey);
                if (credentials == null)
                    throw new InvalidOperationException(
                        string.Format("Missing credentials for the {0} provider", userProfile.ProviderKey));
                
                var profile = service.GetUserProfile(credentials, userProfile.AuthorizationToken);

                if (profile != null)
                {
                    if (!string.IsNullOrWhiteSpace(profile.Name))
                    {
                        dto.Name = profile.Name;
                    }
                    dto.ProfilePictureUrl = profile.ProfilePictureUrl;
                }
            }
            return dtos;
        }
        
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposing) return;

            _userRepository.Dispose();
            _streamFilterRepository.Dispose();
        }
    }
}
