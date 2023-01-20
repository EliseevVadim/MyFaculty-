using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.StudyClubs.Commands.RemoveUsersFromStudyClubByCourse;

namespace MyFaculty.WebApi.Dto
{
    public class RemoveUsersFromStudyClubByCourseDto : IMapWith<RemoveUsersFromStudyClubByCourseCommand>
    {
        public int IssuerId { get; set; }
        public int CourseId { get; set; }
        public int StudyClubId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RemoveUsersFromStudyClubByCourseDto, RemoveUsersFromStudyClubByCourseCommand>()
                .ForMember(command => command.IssuerId, options => options.MapFrom(dto => dto.IssuerId))
                .ForMember(command => command.CourseId, options => options.MapFrom(dto => dto.CourseId))
                .ForMember(command => command.StudyClubId, options => options.MapFrom(dto => dto.StudyClubId));
        }
    }
}
