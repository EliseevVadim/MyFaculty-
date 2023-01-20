using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.StudyClubs.Commands.AddUsersToStudyClubByGroup;

namespace MyFaculty.WebApi.Dto
{
    public class AddUsersToStudyClubByGroupDto : IMapWith<AddUsersToStudyClubByGroupCommand>
    {
        public int IssuerId { get; set; }
        public int GroupId { get; set; }
        public int StudyClubId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddUsersToStudyClubByGroupDto, AddUsersToStudyClubByGroupCommand>()
                .ForMember(command => command.IssuerId, options => options.MapFrom(dto => dto.IssuerId))
                .ForMember(command => command.GroupId, options => options.MapFrom(dto => dto.GroupId))
                .ForMember(command => command.StudyClubId, options => options.MapFrom(dto => dto.StudyClubId));
        }
    }
}
