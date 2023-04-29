using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.TeachersDisciplines.Commands.UpdateTeacherDiscipline
{
    public class UpdateTeacherDisciplineCommand : IRequest<TeacherDisciplineViewModel>
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int DisciplineId { get; set; }
    }
}
