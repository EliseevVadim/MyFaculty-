using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Dto
{
    public class SecondaryObjectTypeLookupDto : IMapWith<SecondaryObjectType>
    {
        public int Id { get; set; }
        public string ObjectTypeName { get; set; }
        public string TypePath { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SecondaryObjectType, SecondaryObjectTypeLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(type => type.Id))
                .ForMember(dto => dto.ObjectTypeName, options => options.MapFrom(type => type.ObjectTypeName))
                .ForMember(dto => dto.TypePath, options => options.MapFrom(type => type.TypePath));
        }
    }
}
