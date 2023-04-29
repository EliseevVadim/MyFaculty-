using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Dto;
using MyFaculty.Domain.Entities;
using System;

namespace MyFaculty.Application.ViewModels
{
    public class CityViewModel : IMapWith<City>
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public int RegionId { get; set; }
        public RegionLookupDto Region { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<City, CityViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(city => city.Id))
                .ForMember(vm => vm.CityName, options => options.MapFrom(city => city.CityName))
                .ForMember(vm => vm.RegionId, options => options.MapFrom(city => city.RegionId))
                .ForMember(vm => vm.Region, options => options.MapFrom(city => city.Region))
                .ForMember(vm => vm.Created, options => options.MapFrom(city => city.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(city => city.Updated));
        }
    }
}
