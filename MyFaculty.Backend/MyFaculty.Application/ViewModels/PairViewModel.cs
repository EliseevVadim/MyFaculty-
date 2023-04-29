using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Dto;
using MyFaculty.Domain.Entities;
using System;

namespace MyFaculty.Application.ViewModels
{
    public class PairViewModel : IMapWith<Pair>
    {
        public int Id { get; set; }
        public string PairName { get; set; }
        public int TeacherId { get; set; }
        public string TeachersFIO { get; set; }
        public int AuditoriumId { get; set; }
        public AuditoriumLookupDto Auditorium { get; set; }
        public int GroupId { get; set; }
        public GroupLookupDto Group { get; set; }
        public string CourseName { get; set; }
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }
        public int DayOfWeekId { get; set; }
        public string DayOfWeek { get; set; }
        public int PairRepeatingId { get; set; }
        public string PairRepeatingName { get; set; }
        public int PairInfoId { get; set; }
        public PairInfoViewModel PairInfo { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Pair, PairViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(pair => pair.Id))
                .ForMember(vm => vm.PairName, options => options.MapFrom(pair => pair.PairName))
                .ForMember(vm => vm.TeacherId, options => options.MapFrom(pair => pair.TeacherId))
                .ForMember(vm => vm.TeachersFIO, options => options.MapFrom(pair => pair.Teacher.FIO))
                .ForMember(vm => vm.AuditoriumId, options => options.MapFrom(pair => pair.AuditoriumId))
                .ForMember(vm => vm.Auditorium, options => options.MapFrom(pair => pair.Auditorium))
                .ForMember(vm => vm.GroupId, options => options.MapFrom(pair => pair.GroupId))
                .ForMember(dto => dto.Group, options => options.MapFrom(pair => pair.Group))
                .ForMember(vm => vm.DisciplineId, options => options.MapFrom(pair => pair.DisciplineId))
                .ForMember(vm => vm.DisciplineName, options => options.MapFrom(pair => pair.Discipline.DisciplineName))
                .ForMember(vm => vm.DayOfWeekId, options => options.MapFrom(pair => pair.DayOfWeekId))
                .ForMember(vm => vm.DayOfWeek, options => options.MapFrom(pair => pair.DayOfWeek.DaysName))
                .ForMember(vm => vm.PairRepeatingId, options => options.MapFrom(pair => pair.PairRepeatingId))
                .ForMember(vm => vm.PairRepeatingName, options => options.MapFrom(pair => pair.PairRepeating.RepeatingName))
                .ForMember(vm => vm.PairInfoId, options => options.MapFrom(pair => pair.PairInfoId))
                .ForMember(vm => vm.PairInfo, options => options.MapFrom(pair => pair.PairInfo))
                .ForMember(vm => vm.Created, options => options.MapFrom(pair => pair.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(pair => pair.Updated));
        }
    }
}
