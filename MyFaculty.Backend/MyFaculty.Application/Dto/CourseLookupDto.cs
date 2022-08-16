using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Dto
{
    public class CourseLookupDto : IMapWith<Course>
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public int CourseNumber { get; set; }
        public List<GroupLookupDto> Groups { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Course, CourseLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(course => course.Id))
                .ForMember(dto => dto.CourseName, options => options.MapFrom(course => course.CourseName))
                .ForMember(dto => dto.CourseNumber, options => options.MapFrom(course => course.CourseNumber))
                .ForMember(dto => dto.Groups, options => options.MapFrom(course => course.Groups));
        }
    }
}
