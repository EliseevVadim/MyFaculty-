using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Groups.Commands.CreateGroup;

namespace MyFaculty.WebApi.Dto
{
    public class CreateGroupDto : IMapWith<CreateGroupCommand>
    {
        public string GroupName { get; set; }
        public int CourseId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateGroupDto, CreateGroupCommand>()
                .ForMember(command => command.GroupName, options => options.MapFrom(dto => dto.GroupName))
                .ForMember(command => command.CourseId, options => options.MapFrom(dto => dto.CourseId));
        }
    }
}
