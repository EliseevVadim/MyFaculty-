using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.InformationPublics.Commands.UnbanInformationPublic;

namespace MyFaculty.WebApi.Dto
{
    public class UnbanInformationPublicDto : IMapWith<UnbanInformationPublicCommand>
    {
        public int UnbannedPublicId { get; set; }
        public string Reason { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UnbanInformationPublicDto, UnbanInformationPublicCommand>()
                .ForMember(command => command.UnbannedPublicId, options => options.MapFrom(dto => dto.UnbannedPublicId))
                .ForMember(command => command.Reason, options => options.MapFrom(dto => dto.Reason));
        }
    }
}
