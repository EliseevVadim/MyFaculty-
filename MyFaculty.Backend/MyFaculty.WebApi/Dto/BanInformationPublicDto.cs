using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.InformationPublics.Commands.BanInformationPublic;

namespace MyFaculty.WebApi.Dto
{
    public class BanInformationPublicDto : IMapWith<BanInformationPublicCommand>
    {
        public int BannedPublicId { get; set; }
        public string Reason { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BanInformationPublicDto, BanInformationPublicCommand>()
                .ForMember(command => command.BannedPublicId, options => options.MapFrom(dto => dto.BannedPublicId))
                .ForMember(command => command.Reason, options => options.MapFrom(dto => dto.Reason));
        }
    }
}
