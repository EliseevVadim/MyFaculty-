using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System.Collections.Generic;

namespace MyFaculty.Application.Dto
{
    public class CourseLookupDto : IMapWith<Course>
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public int CourseNumber { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public List<GroupLookupDto> Groups { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Course, CourseLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(course => course.Id))
                .ForMember(dto => dto.CourseName, options => options.MapFrom(course => course.CourseName))
                .ForMember(dto => dto.CourseNumber, options => options.MapFrom(course => course.CourseNumber))
                .ForMember(vm => vm.FacultyId, options => options.MapFrom(course => course.FacultyId))
                .ForMember(vm => vm.FacultyName, options => options.MapFrom(course => course.Faculty.FacultyName))
                .ForMember(dto => dto.Groups, options => options.MapFrom(course => course.Groups));
        }
    }
}
