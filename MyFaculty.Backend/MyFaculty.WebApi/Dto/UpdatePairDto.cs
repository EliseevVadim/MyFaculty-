using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Pairs.Commands.UpdatePair;

namespace MyFaculty.WebApi.Dto
{
    public class UpdatePairDto : IMapWith<UpdatePairCommand>
    {
        public int Id { get; set; }
        public string PairName { get; set; }
        public int TeacherId { get; set; }
        public int AuditoriumId { get; set; }
        public int GroupId { get; set; }
        public int DisciplineId { get; set; }
        public int DayOfWeekId { get; set; }
        public int PairRepeatingId { get; set; }
        public int PairInfoId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePairDto, UpdatePairCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.PairName, options => options.MapFrom(dto => dto.PairName))
                .ForMember(command => command.TeacherId, options => options.MapFrom(dto => dto.TeacherId))
                .ForMember(command => command.AuditoriumId, options => options.MapFrom(dto => dto.AuditoriumId))
                .ForMember(command => command.GroupId, options => options.MapFrom(dto => dto.GroupId))
                .ForMember(command => command.DisciplineId, options => options.MapFrom(dto => dto.DisciplineId))
                .ForMember(command => command.DayOfWeekId, options => options.MapFrom(dto => dto.DayOfWeekId))
                .ForMember(command => command.PairRepeatingId, options => options.MapFrom(dto => dto.PairRepeatingId))
                .ForMember(command => command.PairInfoId, options => options.MapFrom(dto => dto.PairInfoId));
        }
    }
}
