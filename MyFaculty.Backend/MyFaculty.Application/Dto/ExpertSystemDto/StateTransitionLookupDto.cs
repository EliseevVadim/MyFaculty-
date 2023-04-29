using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Dto.ExpertSystemDto
{
    public class StateTransitionLookupDto : IMapWith<ExpertSystemStateTransition>
    {
        public int Id { get; set; }
        public int InitialStateId { get; set; }
        public int FinalStateId { get; set; }
        public bool IsLast { get; set; }
        public int AnswerId { get; set; }
        public AnswerLookupDto Answer { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ExpertSystemStateTransition, StateTransitionLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(transition => transition.Id))
                .ForMember(dto => dto.InitialStateId, options => options.MapFrom(transition => transition.InitialStateId))
                .ForMember(dto => dto.FinalStateId, options => options.MapFrom(transition => transition.FinalStateId))
                .ForMember(dto => dto.IsLast, options => options.MapFrom(transition => transition.IsLast))
                .ForMember(dto => dto.AnswerId, options => options.MapFrom(transition => transition.AnswerId))
                .ForMember(dto => dto.Answer, options => options.MapFrom(transition => transition.Answer));
        }
    }
}
