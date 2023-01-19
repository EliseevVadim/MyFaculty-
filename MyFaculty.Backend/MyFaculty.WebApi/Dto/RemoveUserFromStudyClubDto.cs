using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.StudyClubs.Commands.RemoveUserFromStudyClub;

namespace MyFaculty.WebApi.Dto
{
    public class RemoveUserFromStudyClubDto : IMapWith<RemoveUserFromStudyClubCommand>
    {
        public int IssuerId { get; set; }
        public int RemovingUserId { get; set; }
        public int StudyClubId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<RemoveUserFromStudyClubDto, RemoveUserFromStudyClubCommand>()
                .ForMember(command => command.IssuerId, options => options.MapFrom(dto => dto.IssuerId))
                .ForMember(command => command.StudyClubId, options => options.MapFrom(dto => dto.StudyClubId))
                .ForMember(command => command.RemovingUserId, options => options.MapFrom(dto => dto.RemovingUserId));
        }
    }
}
