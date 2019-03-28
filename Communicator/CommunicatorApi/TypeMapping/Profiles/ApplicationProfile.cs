using AutoMapper;
using Swaksoft.Application.Seedwork.Extensions;
using Swaksoft.Domain.Communicator.MessagingModule.Aggregates.VerificationCodeAgg;

namespace Swaksoft.Application.Communicator.TypeMapping.Profiles
{
    public class ApplicationProfile : Profile
    {
        protected override void Configure()
        {
            MapperExtensions.CreateActionResultMap<VerificationCode, Dto.VerificationCodeResult>()
                   .ForMember(dto => dto.VerificationCode, c => c.MapFrom(entity => entity.Code));
        }
    }
}
