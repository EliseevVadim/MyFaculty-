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
    public class RegionLookupDto : IMapWith<Region>
    {
        public int Id { get; set; }
        public string RegionName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public List<CityLookupDto> Cities { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Region, RegionLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(region => region.Id))
                .ForMember(dto => dto.RegionName, options => options.MapFrom(region => region.RegionName))
                .ForMember(dto => dto.CountryId, options => options.MapFrom(region => region.CountryId))
                .ForMember(dto => dto.CountryName, options => options.MapFrom(region => region.Country.CountryName))
                .ForMember(dto => dto.Cities, options => options.MapFrom(region => region.Cities));
        }
    }
}
