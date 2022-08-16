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
    public class TeacherDisciplineLookupDto : IMapWith<TeacherDiscipline>
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string TeachersFIO { get; set; }
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TeacherDiscipline, TeacherDisciplineLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(td => td.Id))
                .ForMember(dto => dto.TeacherId, options => options.MapFrom(td => td.TeacherId))
                .ForMember(dto => dto.TeachersFIO, options => options.MapFrom(td => td.Teacher.FIO))
                .ForMember(dto => dto.DisciplineId, options => options.MapFrom(td => td.DisciplineId))
                .ForMember(dto => dto.DisciplineName, options => options.MapFrom(td => td.Discipline.DisciplineName));
        }
    }
}
