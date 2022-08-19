using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Groups.Commands.UpdateGroup;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateGroupDto : IMapWith<UpdateGroupCommand>
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int CourseId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateGroupDto, UpdateGroupCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.GroupName, options => options.MapFrom(dto => dto.GroupName))
                .ForMember(command => command.CourseId, options => options.MapFrom(dto => dto.CourseId));
        }
    }
}
