using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;

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
