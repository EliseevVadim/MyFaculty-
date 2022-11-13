using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Cities.Commands.UpdateCity;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateCityDto : IMapWith<UpdateCityCommand>
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public int RegionId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCityDto, UpdateCityCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.CityName, options => options.MapFrom(dto => dto.CityName))
                .ForMember(command => command.RegionId, options => options.MapFrom(dto => dto.RegionId));
        }
    }
}
