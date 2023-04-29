using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Dto
{
    public class CityLookupDto : IMapWith<City>
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<City, CityLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(city => city.Id))
                .ForMember(dto => dto.CityName, options => options.MapFrom(city => city.CityName))
                .ForMember(dto => dto.CountryId, options => options.MapFrom(city => city.Region.CountryId))
                .ForMember(dto => dto.CountryName, options => options.MapFrom(city => city.Region.Country.CountryName))
                .ForMember(dto => dto.RegionId, options => options.MapFrom(city => city.RegionId))
                .ForMember(dto => dto.RegionName, options => options.MapFrom(city => city.Region.RegionName));
        }
    }
}
