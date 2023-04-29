using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Dto;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class FacultyViewModel : IMapWith<Faculty>
    {
        public int Id { get; set; }
        public string FacultyName { get; set; }
        public string OfficialWebsite { get; set; }
        public List<FloorLookupDto> Floors { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Faculty, FacultyViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(faculty => faculty.Id))
                .ForMember(vm => vm.FacultyName, options => options.MapFrom(faculty => faculty.FacultyName))
                .ForMember(vm => vm.OfficialWebsite, options => options.MapFrom(faculty => faculty.OfficialWebsite))
                .ForMember(vm => vm.Floors, options => options.MapFrom(faculty => faculty.Floors))
                .ForMember(vm => vm.Created, options => options.MapFrom(faculty => faculty.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(faculty => faculty.Updated));
        }
    }
}
