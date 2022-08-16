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
    public class WorkDayOfWeekLookupDto : IMapWith<WorkDayOfWeek>
    {
        public int Id { get; set; }
        public string DaysName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<WorkDayOfWeek, WorkDayOfWeekLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(day => day.Id))
                .ForMember(dto => dto.DaysName, options => options.MapFrom(day => day.DaysName));
        }
    }
}
