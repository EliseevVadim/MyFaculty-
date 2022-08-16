using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.ViewModels
{
    public class GroupViewModel : IMapWith<Group>
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Group, GroupViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(group => group.Id))
                .ForMember(vm => vm.GroupName, options => options.MapFrom(group => group.GroupName))
                .ForMember(vm => vm.CourseId, options => options.MapFrom(group => group.CourseId))
                .ForMember(vm => vm.CourseName, options => options.MapFrom(group => group.Course.CourseName))
                .ForMember(vm => vm.Created, options => options.MapFrom(group => group.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(group => group.Updated));
        }
    }
}
