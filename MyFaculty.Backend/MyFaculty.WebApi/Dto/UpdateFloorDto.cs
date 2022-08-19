using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Floors.Commands.UpdateFloor;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateFloorDto : IMapWith<UpdateFloorCommand>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bounds { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateFloorDto, UpdateFloorCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.Name, options => options.MapFrom(dto => dto.Name))
                .ForMember(command => command.Bounds, options => options.MapFrom(dto => dto.Bounds));
        }
    }
}
