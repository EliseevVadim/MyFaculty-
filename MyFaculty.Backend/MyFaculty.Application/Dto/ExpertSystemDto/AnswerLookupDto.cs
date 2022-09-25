using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Dto.ExpertSystemDto
{
    public class AnswerLookupDto : IMapWith<ExpertSystemAnswer>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int StateTransitionId { get; set; }
        public int StateId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ExpertSystemAnswer, AnswerLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(answer => answer.Id))
                .ForMember(dto => dto.Text, options => options.MapFrom(answer => answer.Text))
                .ForMember(dto => dto.StateTransitionId, options => options.MapFrom(answer => answer.StateTransitionId))
                .ForMember(dto => dto.StateId, options => options.MapFrom(answer => answer.StateId));
        }
    }
}
