using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Cities.Commands.UpdateCity;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateCityDto : IMapWith<UpdateCityCommand>
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCityCommand, UpdateCityCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.CityName, options => options.MapFrom(dto => dto.CityName))
                .ForMember(command => command.CountryId, options => options.MapFrom(dto => dto.CountryId));
        }
    }
}
