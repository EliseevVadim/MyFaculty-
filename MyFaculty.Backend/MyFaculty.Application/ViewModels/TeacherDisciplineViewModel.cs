using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;

namespace MyFaculty.Application.ViewModels
{
    public class TeacherDisciplineViewModel : IMapWith<TeacherDiscipline>
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string TeachersFIO { get; set; }
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TeacherDiscipline, TeacherDisciplineViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(td => td.Id))
                .ForMember(vm => vm.TeacherId, options => options.MapFrom(td => td.TeacherId))
                .ForMember(vm => vm.TeachersFIO, options => options.MapFrom(td => td.Teacher.FIO))
                .ForMember(vm => vm.DisciplineId, options => options.MapFrom(td => td.DisciplineId))
                .ForMember(vm => vm.DisciplineName, options => options.MapFrom(td => td.Discipline.DisciplineName))
                .ForMember(vm => vm.Created, options => options.MapFrom(td => td.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(td => td.Updated));
        }
    }
}
