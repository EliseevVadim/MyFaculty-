using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Auditoriums.Commands.CreateAuditorium;

namespace MyFaculty.WebApi.Dto
{
    public class CreateAuditoriumDto : IMapWith<CreateAuditoriumCommand>
    {
        public string AuditoriumName { get; set; }
        public string PositionInfo { get; set; }
        public int FloorId { get; set; }
        public int TeacherId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAuditoriumDto, CreateAuditoriumCommand>()
                .ForMember(command => command.AuditoriumName, options => options.MapFrom(dto => dto.AuditoriumName))
                .ForMember(command => command.PositionInfo, options => options.MapFrom(dto => dto.PositionInfo))
                .ForMember(command => command.FloorId, options => options.MapFrom(dto => dto.FloorId))
                .ForMember(command => command.TeacherId, options => options.MapFrom(dto => dto.TeacherId));
        }
    }
}
