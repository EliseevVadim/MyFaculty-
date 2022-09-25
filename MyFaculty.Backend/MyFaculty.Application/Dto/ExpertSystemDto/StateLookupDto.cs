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
    public class StateLookupDto : IMapWith<ExpertSystemState>
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Explanation { get; set; }
        public List<AnswerLookupDto> Answers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ExpertSystemState, StateLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(state => state.Id))
                .ForMember(dto => dto.Question, options => options.MapFrom(state => state.Question))
                .ForMember(dto => dto.Explanation, options => options.MapFrom(state => state.Explanation))
                .ForMember(dto => dto.Answers, options => options.MapFrom(state => state.Answers));
        }
    }
}
