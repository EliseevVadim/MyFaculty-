using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Users.Commands.BanUser;

namespace MyFaculty.WebApi.Dto
{
    public class BanUserDto : IMapWith<BanUserCommand>
    {
        public int BannedUserId { get; set; }
        public string Reason { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BanUserDto, BanUserCommand>()
                .ForMember(command => command.BannedUserId, options => options.MapFrom(dto => dto.BannedUserId))
                .ForMember(command => command.Reason, options => options.MapFrom(dto => dto.Reason));
        }
    }
}
