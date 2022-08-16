using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Dto
{
    public class PairInfoLookupDto : IMapWith<PairInfo>
    {
        public int Id { get; set; }
        public int PairNumber { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PairInfo, PairInfoLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(info => info.Id))
                .ForMember(dto => dto.PairNumber, options => options.MapFrom(info => info.PairNumber))
                .ForMember(dto => dto.StartTime, options => options.MapFrom(info => info.StartTime))
                .ForMember(dto => dto.EndTime, options => options.MapFrom(info => info.EndTime));
        }
    }
}
