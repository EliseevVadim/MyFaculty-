using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Floors.Commands.CreateFloor;

namespace MyFaculty.WebApi.Dto
{
    public class CreateFloorDto : IMapWith<CreateFloorCommand>
    {
        public string Name { get; set; }
        public string Bounds { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFloorDto, CreateFloorCommand>()
                .ForMember(command => command.Name, options => options.MapFrom(dto => dto.Name))
                .ForMember(command => command.Bounds, options => options.MapFrom(dto => dto.Bounds));
        }
    }
}
