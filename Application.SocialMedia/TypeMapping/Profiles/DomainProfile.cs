using AutoMapper;
using Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.UserProfileAgg;
using Events = Swaksoft.Domain.SocialMedia.SocialModule.Events;

namespace Swaksoft.Application.SocialMedia.TypeMapping.Profiles
{
    public class DomainProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Events.StreamedMessageSent, TwitterUserProfileLog>();
        }
    }
}
