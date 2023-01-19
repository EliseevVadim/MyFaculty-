using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.StudyClubs.Commands.AddModeratorToStudyClub;

namespace MyFaculty.WebApi.Dto
{
    public class AddModeratorToStudyClubDto : IMapWith<AddModeratorToStudyClubCommand>
    {
        public int IssuerId { get; set; }
        public int StudyClubId { get; set; }
        public int ModeratorId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddModeratorToStudyClubDto, AddModeratorToStudyClubCommand>()
                .ForMember(command => command.IssuerId, options => options.MapFrom(dto => dto.IssuerId))
                .ForMember(command => command.StudyClubId, options => options.MapFrom(dto => dto.StudyClubId))
                .ForMember(command => command.ModeratorId, options => options.MapFrom(dto => dto.ModeratorId));
        }
    }
}
