using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.InformationPublics.Commands.DemoteInformationPublicModerator;

namespace MyFaculty.WebApi.Dto
{
    public class DemoteInformationPublicModeratorDto : IMapWith<DemoteInformationPublicModeratorCommand>
    {
        public int IssuerId { get; set; }
        public int InformationPublicId { get; set; }
        public int ModeratorId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DemoteInformationPublicModeratorDto, DemoteInformationPublicModeratorCommand>()
                .ForMember(command => command.IssuerId, options => options.MapFrom(dto => dto.IssuerId))
                .ForMember(command => command.InformationPublicId, options => options.MapFrom(dto => dto.InformationPublicId))
                .ForMember(command => command.ModeratorId, options => options.MapFrom(dto => dto.ModeratorId));
        }
    }
}
