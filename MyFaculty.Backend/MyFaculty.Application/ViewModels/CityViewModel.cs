using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Dto;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.ViewModels
{
    public class CityViewModel : IMapWith<City>
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }
        public CountryLookupDto Country { get; set; }
        // TODO: add AppUsers
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<City, CityViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(city => city.Id))
                .ForMember(vm => vm.CityName, options => options.MapFrom(city => city.CityName))
                .ForMember(vm => vm.CountryId, options => options.MapFrom(city => city.CountryId))
                .ForMember(vm => vm.Country, options => options.MapFrom(city => city.Country))
                .ForMember(vm => vm.Created, options => options.MapFrom(city => city.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(city => city.Updated));
        }
    }
}
