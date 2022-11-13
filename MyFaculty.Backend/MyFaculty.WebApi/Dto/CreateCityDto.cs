using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Cities.Commands.CreateCity;

namespace MyFaculty.WebApi.Dto
{
    public class CreateCityDto : IMapWith<CreateCityCommand>
    {
        public string CityName { get; set; }
        public int RegionId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCityDto, CreateCityCommand>()
                .ForMember(command => command.CityName, options => options.MapFrom(dto => dto.CityName))
                .ForMember(command => command.RegionId, options => options.MapFrom(dto => dto.RegionId));
        }
    }
}
