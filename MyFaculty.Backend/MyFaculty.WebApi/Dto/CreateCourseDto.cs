using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Courses.Commands.CreateCourse;

namespace MyFaculty.WebApi.Dto
{
    public class CreateCourseDto : IMapWith<CreateCourseCommand>
    {
        public string CourseName { get; set; }
        public int CourseNumber { get; set; }
        public int FacultyId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCourseDto, CreateCourseCommand>()
                .ForMember(command => command.CourseNumber, options => options.MapFrom(dto => dto.CourseNumber))
                .ForMember(command => command.CourseName, options => options.MapFrom(dto => dto.CourseName))
                .ForMember(command => command.FacultyId, options => options.MapFrom(dto => dto.FacultyId));
        }
    }
}
