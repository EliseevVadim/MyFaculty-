using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.SecondaryObjectTypes.Commands.UpdateSecondaryObjectType;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateSecondaryObjectTypeDto : IMapWith<UpdateSecondaryObjectTypeCommand>
    {
        public int Id { get; set; }
        public string ObjectTypeName { get; set; }
        public string TypePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateSecondaryObjectTypeDto, UpdateSecondaryObjectTypeCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.ObjectTypeName, options => options.MapFrom(dto => dto.ObjectTypeName))
                .ForMember(command => command.TypePath, options => options.MapFrom(dto => dto.TypePath));
        }
    }
}
