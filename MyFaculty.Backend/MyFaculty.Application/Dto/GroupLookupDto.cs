using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace MyFaculty.Application.Dto
{
    public class GroupLookupDto : IMapWith<Group>
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Group, GroupLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(group => group.Id))
                .ForMember(dto => dto.GroupName, options => options.MapFrom(group => group.GroupName))
                .ForMember(dto => dto.CourseId, options => options.MapFrom(group => group.CourseId))
                .ForMember(dto => dto.CourseName, options => options.MapFrom(group => group.Course.CourseName))
                .ForMember(dto => dto.FacultyId, options => options.MapFrom(group => group.Course.FacultyId))
                .ForMember(dto => dto.FacultyName, options => options.MapFrom(group => group.Course.Faculty.FacultyName));
        }
    }
}
