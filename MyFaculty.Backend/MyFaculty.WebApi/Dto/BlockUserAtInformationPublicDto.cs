using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.InformationPublics.Commands.BlockUserAtInformationPublic;

namespace MyFaculty.WebApi.Dto
{
    public class BlockUserAtInformationPublicDto : IMapWith<BlockUserAtInformationPublicCommand>
    {
        public int PublicId { get; set; }
        public int UserId { get; set; }
        public int IssuerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BlockUserAtInformationPublicDto, BlockUserAtInformationPublicCommand>()
                .ForMember(command => command.PublicId, options => options.MapFrom(dto => dto.PublicId))
                .ForMember(command => command.UserId, options => options.MapFrom(dto => dto.UserId))
                .ForMember(command => command.IssuerId, options => options.MapFrom(dto => dto.IssuerId));
        }
    }
}
