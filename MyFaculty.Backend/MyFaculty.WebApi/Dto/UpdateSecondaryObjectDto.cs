using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.SecondaryObjects.Commands.UpdateSecondaryObject;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateSecondaryObjectDto : IMapWith<UpdateSecondaryObjectCommand>
    {
        public int Id { get; set; }
        public string ObjectName { get; set; }
        public string PositionInfo { get; set; }
        public int SecondaryObjectTypeId { get; set; }
        public int FloorId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateSecondaryObjectDto, UpdateSecondaryObjectCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.ObjectName, options => options.MapFrom(dto => dto.ObjectName))
                .ForMember(command => command.PositionInfo, options => options.MapFrom(dto => dto.PositionInfo))
                .ForMember(command => command.SecondaryObjectTypeId, options => options.MapFrom(dto => dto.SecondaryObjectTypeId))
                .ForMember(command => command.FloorId, options => options.MapFrom(dto => dto.FloorId));
        }
    }
}
