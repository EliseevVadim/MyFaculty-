using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;

namespace MyFaculty.Application.ViewModels
{
    public class SecondaryObjectTypeViewModel : IMapWith<SecondaryObjectType>
    {
        public int Id { get; set; }
        public string ObjectTypeName { get; set; }
        public string TypePath { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SecondaryObjectType, SecondaryObjectTypeViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(type => type.Id))
                .ForMember(vm => vm.ObjectTypeName, options => options.MapFrom(type => type.ObjectTypeName))
                .ForMember(vm => vm.TypePath, options => options.MapFrom(type => type.TypePath))
                .ForMember(vm => vm.Created, options => options.MapFrom(type => type.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(type => type.Updated));
        }
    }
}
