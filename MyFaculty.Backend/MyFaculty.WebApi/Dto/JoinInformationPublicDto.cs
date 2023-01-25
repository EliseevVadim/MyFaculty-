using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.InformationPublics.Commands.JoinInformationPublic;

namespace MyFaculty.WebApi.Dto
{
    public class JoinInformationPublicDto : IMapWith<JoinInformationPublicCommand>
    {
        public int UserId { get; set; }
        public int PublicId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JoinInformationPublicDto, JoinInformationPublicCommand>()
                .ForMember(command => command.UserId, options => options.MapFrom(dto => dto.UserId))
                .ForMember(command => command.PublicId, options => options.MapFrom(dto => dto.PublicId));
        }
    }
}
