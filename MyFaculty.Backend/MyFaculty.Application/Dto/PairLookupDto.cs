using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Dto
{
    public class PairLookupDto : IMapWith<Pair>
    {
        public int Id { get; set; }
        public string PairName { get; set; }
        public int TeacherId { get; set; }
        public string TeachersFIO { get; set; }
        public int AuditoriumId { get; set; }
        public Auditorium Auditorium { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int DisciplineId { get; set; }
        public Discipline Discipline { get; set; }
        public int DayOfWeekId { get; set; }
        public WorkDayOfWeekLookupDto DayOfWeek { get; set; }
        public int PairRepeatingId { get; set; }
        public PairRepeatingLookupDto PairRepeating { get; set; }
        public int PairInfoId { get; set; }
        public PairInfoViewModel PairInfo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Pair, PairLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(pair => pair.Id))
                .ForMember(dto => dto.PairName, options => options.MapFrom(pair => pair.PairName))
                .ForMember(dto => dto.TeacherId, options => options.MapFrom(pair => pair.TeacherId))
                .ForMember(dto => dto.TeachersFIO, options => options.MapFrom(pair => pair.Teacher.FIO))
                .ForMember(dto => dto.AuditoriumId, options => options.MapFrom(pair => pair.AuditoriumId))
                .ForMember(dto => dto.Auditorium, options => options.MapFrom(pair => pair.Auditorium))
                .ForMember(dto => dto.GroupId, options => options.MapFrom(pair => pair.GroupId))
                .ForMember(dto => dto.GroupName, options => options.MapFrom(pair => pair.Group.GroupName))
                .ForMember(dto => dto.DisciplineId, options => options.MapFrom(pair => pair.DisciplineId))
                .ForMember(dto => dto.Discipline, options => options.MapFrom(pair => pair.Discipline))
                .ForMember(dto => dto.DayOfWeekId, options => options.MapFrom(pair => pair.DayOfWeekId))
                .ForMember(dto => dto.DayOfWeek, options => options.MapFrom(pair => pair.DayOfWeek))
                .ForMember(dto => dto.PairRepeatingId, options => options.MapFrom(pair => pair.PairRepeatingId))
                .ForMember(dto => dto.PairRepeating, options => options.MapFrom(pair => pair.PairRepeating))
                .ForMember(dto => dto.PairInfoId, options => options.MapFrom(pair => pair.PairInfoId))
                .ForMember(dto => dto.PairInfo, options => options.MapFrom(pair => pair.PairInfo));
        }
    }
}
