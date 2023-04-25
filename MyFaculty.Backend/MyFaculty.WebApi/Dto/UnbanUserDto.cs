using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Users.Commands.UnbanUser;

namespace MyFaculty.WebApi.Dto
{
    public class UnbanUserDto : IMapWith<UnbanUserCommand>
    {
        public int UnbannedUserId { get; set; }
        public string Reason { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UnbanUserDto, UnbanUserCommand>()
                .ForMember(command => command.UnbannedUserId, options => options.MapFrom(dto => dto.UnbannedUserId))
                .ForMember(command => command.Reason, options => options.MapFrom(dto => dto.Reason));
        }
    }
}
