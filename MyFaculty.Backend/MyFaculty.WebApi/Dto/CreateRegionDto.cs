using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Regions.Commands.CreateRegion;

namespace MyFaculty.WebApi.Dto
{
    public class CreateRegionDto : IMapWith<CreateRegionCommand>
    {
        public string RegionName { get; set; }
        public int CountryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateRegionDto, CreateRegionCommand>()
                .ForMember(command => command.RegionName, options => options.MapFrom(dto => dto.RegionName))
                .ForMember(command => command.CountryId, options => options.MapFrom(dto => dto.CountryId));
        }
    }
}
