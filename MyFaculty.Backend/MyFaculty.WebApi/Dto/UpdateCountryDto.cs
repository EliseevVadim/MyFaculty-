using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Countries.Commands.UpdateCountry;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateCountryDto : IMapWith<UpdateCountryCommand>
    {
        public int Id { get; set; }
        public string CountryName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCountryDto, UpdateCountryCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.CountryName, options => options.MapFrom(dto => dto.CountryName));
        }
    }
}
