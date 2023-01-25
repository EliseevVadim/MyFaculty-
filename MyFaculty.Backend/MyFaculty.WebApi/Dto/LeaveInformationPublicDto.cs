using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.InformationPublics.Commands.LeaveInformationPublic;

namespace MyFaculty.WebApi.Dto
{
    public class LeaveInformationPublicDto : IMapWith<LeaveInformationPublicCommand>
    {
        public int UserId { get; set; }
        public int PublicId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LeaveInformationPublicDto, LeaveInformationPublicCommand>()
                .ForMember(command => command.UserId, options => options.MapFrom(dto => dto.UserId))
                .ForMember(command => command.PublicId, options => options.MapFrom(dto => dto.PublicId));
        }
    }
}
