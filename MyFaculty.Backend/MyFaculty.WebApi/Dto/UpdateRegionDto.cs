using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Regions.Commands.UpdateRegion;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateRegionDto : IMapWith<UpdateRegionCommand>
    {
        public int Id { get; set; }
        public string RegionName { get; set; }
        public int CountryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateRegionDto, UpdateRegionCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.RegionName, options => options.MapFrom(dto => dto.RegionName))
                .ForMember(command => command.CountryId, options => options.MapFrom(dto => dto.CountryId));
        }
    }
}
