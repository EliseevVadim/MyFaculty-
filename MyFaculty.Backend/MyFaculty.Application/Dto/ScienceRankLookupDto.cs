using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Dto
{
    public class ScienceRankLookupDto : IMapWith<ScienceRank>
    {
        public int Id { get; set; }
        public string RankName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ScienceRank, ScienceRankLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(rank => rank.Id))
                .ForMember(dto => dto.RankName, options => options.MapFrom(rank => rank.RankName));
        }
    }
}
