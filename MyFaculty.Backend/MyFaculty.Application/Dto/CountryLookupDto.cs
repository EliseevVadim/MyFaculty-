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
    public class CountryLookupDto : IMapWith<Country>
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public List<RegionLookupDto> Regions { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Country, CountryLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(country => country.Id))
                .ForMember(dto => dto.CountryName, options => options.MapFrom(country => country.CountryName))
                .ForMember(dto => dto.Regions, options => options.MapFrom(country => country.Regions));
        }
    }
}
