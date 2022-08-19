using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.TeachersDisciplines.Commands.CreateTeacherDiscipline;

namespace MyFaculty.WebApi.Dto
{
    public class CreateTeacherDisciplineDto : IMapWith<CreateTeacherDisciplineCommand>
    {
        public int TeacherId { get; set; }
        public int DisciplineId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTeacherDisciplineDto, CreateTeacherDisciplineCommand>()
                .ForMember(command => command.TeacherId, options => options.MapFrom(dto => dto.TeacherId))
                .ForMember(command => command.DisciplineId, options => options.MapFrom(dto => dto.DisciplineId));
        }
    }
}
