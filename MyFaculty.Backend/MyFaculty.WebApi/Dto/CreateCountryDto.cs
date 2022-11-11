using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Countries.Commands.CreateCountry;

namespace MyFaculty.WebApi.Dto
{
    public class CreateCountryDto : IMapWith<CreateCountryCommand>
    {
        public string CountryName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCountryDto, CreateCountryCommand>()
                .ForMember(command => command.CountryName, options => options.MapFrom(dto => dto.CountryName));
        }
    }
}
