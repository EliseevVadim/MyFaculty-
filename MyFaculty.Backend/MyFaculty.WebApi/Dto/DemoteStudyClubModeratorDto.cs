using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.StudyClubs.Commands.DemoteStudyClubModerator;

namespace MyFaculty.WebApi.Dto
{
    public class DemoteStudyClubModeratorDto : IMapWith<DemoteStudyClubModeratorCommand>
    {
        public int IssuerId { get; set; }
        public int StudyClubId { get; set; }
        public int ModeratorId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DemoteStudyClubModeratorDto, DemoteStudyClubModeratorCommand>()
                .ForMember(command => command.IssuerId, options => options.MapFrom(dto => dto.IssuerId))
                .ForMember(command => command.StudyClubId, options => options.MapFrom(dto => dto.StudyClubId))
                .ForMember(command => command.ModeratorId, options => options.MapFrom(dto => dto.ModeratorId));
        }
    }
}
