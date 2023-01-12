using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.StudyClubs.Commands.LeaveStudyClub;

namespace MyFaculty.WebApi.Dto
{
    public class LeaveStudyClubDto : IMapWith<LeaveStudyClubCommand>
    {
        public int UserId { get; set; }
        public int StudyClubId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LeaveStudyClubDto, LeaveStudyClubCommand>()
                .ForMember(command => command.UserId, options => options.MapFrom(dto => dto.UserId))
                .ForMember(command => command.StudyClubId, options => options.MapFrom(dto => dto.StudyClubId));
        }
    }
}
