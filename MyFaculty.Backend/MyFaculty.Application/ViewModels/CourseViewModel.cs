using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.ViewModels
{
    public class CourseViewModel : IMapWith<Course>
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public int CourseNumber { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public List<GroupViewModel> Groups { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Course, CourseViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(course => course.Id))
                .ForMember(vm => vm.CourseName, options => options.MapFrom(course => course.CourseName))
                .ForMember(vm => vm.CourseNumber, options => options.MapFrom(course => course.CourseNumber))
                .ForMember(vm => vm.FacultyId, options => options.MapFrom(course => course.FacultyId))
                .ForMember(vm => vm.FacultyName, options => options.MapFrom(course => course.Faculty.FacultyName))
                .ForMember(vm => vm.Groups, options => options.MapFrom(course => course.Groups))
                .ForMember(vm => vm.Created, options => options.MapFrom(course => course.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(course => course.Updated));
        }
    }
}
