using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Dto;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class RegionViewModel : IMapWith<Region>
    {
        public int Id { get; set; }
        public string RegionName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public List<CityLookupDto> Cities { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Region, RegionViewModel>()
                .ForMember(dto => dto.Id, options => options.MapFrom(region => region.Id))
                .ForMember(dto => dto.RegionName, options => options.MapFrom(region => region.RegionName))
                .ForMember(dto => dto.CountryId, options => options.MapFrom(region => region.CountryId))
                .ForMember(dto => dto.CountryName, options => options.MapFrom(region => region.Country.CountryName))
                .ForMember(dto => dto.Cities, options => options.MapFrom(region => region.Cities))
                .ForMember(dto => dto.Created, options => options.MapFrom(region => region.Created))
                .ForMember(dto => dto.Updated, options => options.MapFrom(region => region.Updated));
        }
    }
}
