using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.InformationPublics.Commands.UnblockUserAtInformationPublic;

namespace MyFaculty.WebApi.Dto
{
    public class UnblockUserAtInformationPublicDto : IMapWith<UnblockUserAtInformationPublicCommand>
    {
        public int PublicId { get; set; }
        public int UserId { get; set; }
        public int IssuerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UnblockUserAtInformationPublicDto, UnblockUserAtInformationPublicCommand>()
                .ForMember(command => command.PublicId, options => options.MapFrom(dto => dto.PublicId))
                .ForMember(command => command.UserId, options => options.MapFrom(dto => dto.UserId))
                .ForMember(command => command.IssuerId, options => options.MapFrom(dto => dto.IssuerId));
        }
    }
}
