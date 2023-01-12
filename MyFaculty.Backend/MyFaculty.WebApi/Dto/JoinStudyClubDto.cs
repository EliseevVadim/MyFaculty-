using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.StudyClubs.Commands.JoinStudyClub;

namespace MyFaculty.WebApi.Dto
{
    public class JoinStudyClubDto : IMapWith<JoinStudyClubCommand>
    {
        public int UserId { get; set; }
        public int StudyClubId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JoinStudyClubDto, JoinStudyClubCommand>()
                .ForMember(command => command.UserId, options => options.MapFrom(dto => dto.UserId))
                .ForMember(command => command.StudyClubId, options => options.MapFrom(dto => dto.StudyClubId));
        }
    }
}
