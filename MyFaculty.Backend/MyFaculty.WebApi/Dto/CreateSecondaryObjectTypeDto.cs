using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.SecondaryObjectTypes.Commands.CreateSecondaryObjectType;

namespace MyFaculty.WebApi.Dto
{
    public class CreateSecondaryObjectTypeDto : IMapWith<CreateSecondaryObjectTypeCommand>
    {
        public string ObjectTypeName { get; set; }
        public string TypePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateSecondaryObjectTypeDto, CreateSecondaryObjectTypeCommand>()
                .ForMember(command => command.ObjectTypeName, options => options.MapFrom(dto => dto.ObjectTypeName))
                .ForMember(command => command.TypePath, options => options.MapFrom(dto => dto.TypePath));
        }
    }
}
