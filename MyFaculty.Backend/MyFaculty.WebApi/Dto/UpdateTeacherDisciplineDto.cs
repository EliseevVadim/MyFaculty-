using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.TeachersDisciplines.Commands.UpdateTeacherDiscipline;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateTeacherDisciplineDto : IMapWith<UpdateTeacherDisciplineCommand>
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int DisciplineId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateTeacherDisciplineDto, UpdateTeacherDisciplineCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.TeacherId, options => options.MapFrom(dto => dto.TeacherId))
                .ForMember(command => command.DisciplineId, options => options.MapFrom(dto => dto.DisciplineId));
        }
    }
}
