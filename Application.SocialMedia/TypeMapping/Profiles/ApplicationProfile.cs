using AutoMapper;
using Swaksoft.Application.Seedwork.Extensions;
using Swaksoft.Application.Seedwork.TypeMapping;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.EventAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.MessageOperationAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Swaksoft.Domain.SocialMedia.SocialModule.Contracts.Streaming;
using Address = Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.AddressAgg.Address;
using Events = Swaksoft.Domain.SocialMedia.SocialModule.Events;
using Contracts = Swaksoft.Domain.SocialMedia.SocialModule.Contracts;
using Message = Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.MessageAgg.Message;
using StreamedTweet = Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg.StreamedTweet;
using StreamedTweetOptions = Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamedTweetAgg.StreamedTweetOptions;
using StreamFilter = Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.StreamFilterAgg.StreamFilter;

namespace Swaksoft.Application.SocialMedia.TypeMapping.Profiles
{
    public class ApplicationProfile : AutoMapperProfile
    {
        public ApplicationProfile()
        {
            CreateActionResultMap<TwitterUserProfile, Dto.UserProfileResult>();
            CreateActionResultMap<FoursquareUserProfile, Dto.UserProfileResult>();
            CreateActionResultMap<StreamFilter, Dto.StreamFilterResult>();
            CreateActionResultMap<Message, Dto.MessageResult>();
            CreateActionResultMap<StreamFilterMessageOperation, Dto.MessageOperationResult>()
                    .ForMember(dto => dto.MessageId, c => c.MapFrom(entity => entity.Message.Id))
                    .ForMember(dto => dto.MessageOperationId, c => c.MapFrom(entity => entity.Id))
                    .ForMember(dto => dto.Type, c => c.UseValue(Dto.MessageOperationType.StreamFilter));
            CreateActionResultMap<DirectMessageOperation, Dto.MessageOperationResult>()
                   .ForMember(dto => dto.MessageId, c => c.MapFrom(entity => entity.Message.Id))
                   .ForMember(dto => dto.MessageOperationId, c => c.MapFrom(entity => entity.Id))
                   .ForMember(dto => dto.Type, c => c.UseValue(Dto.MessageOperationType.StreamFilter));

            CreateMap<IClient, Dto.StreamingClient>()
                  .ForMember(dto => dto.ClientName, c => c.MapFrom(entity => entity.Settings.ClientName));

           CreateMap<TwitterUserProfile, Dto.UserProfile>()
                .ForMember(dto => dto.ProviderKey, c => c.MapFrom(entity => entity.ProviderKey.ToString()));
            CreateMap<FoursquareUserProfile, Dto.UserProfile>()
               .ForMember(dto => dto.ProviderKey, c => c.MapFrom(entity => entity.ProviderKey.ToString()));
            CreateMap<StreamFilterMessageOperation, Dto.MessageOperation>()
                .ForMember(dto => dto.MessageOperationId, c => c.MapFrom(entity => entity.Id))
                .ForMember(dto => dto.MessageId, c => c.MapFrom(entity => entity.MessageId));

            CreateMap<StreamedTweet, Dto.StreamedTweet>();

            CreateMap<Message, Dto.Message>();
            CreateMap<StreamFilter, Dto.StreamFilter>()
                .ForMember(dto => dto.MessageOperations, c => c.MapFrom(entity => entity.StreamFilterMessageOperations));


            CreateMap<Dto.StreamedTweetOptions, StreamedTweetOptions>();

            CreateMap<Events.MessageSent, Dto.TweetSent>();

            CreateMap<Contracts.AccessTokenResult, Dto.AccessTokenResult>();

            CreateActionResultMap<Address, Dto.AddressResult>();
            CreateMap<Address, Dto.Address>();

            CreateActionResultMap<Event, Dto.EventResult>()
                .ForMember(dto => dto.Instructor, c => c.MapFrom(entity => entity.User.Name.ToString()));
            CreateActionResultMap<Event, Dto.EventWithAddressResult>()
                .ForMember(dto => dto.Instructor, c => c.MapFrom(entity => entity.User.Name.ToString()));
            CreateMap<Event, Dto.Event>()
                .ForMember(dto => dto.Instructor, c=>c.MapFrom(entity => entity.User.Name.ToString()));
            CreateMap<Event, Dto.EventWithAddress>()
               .ForMember(dto => dto.Instructor, c => c.MapFrom(entity => entity.User.Name.ToString()));

            CreateMap<Events.Streaming.DisplayActivityOnMap, Dto.DisplayActivityOnMap>();
        }
        
    }
}
