using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.TeachersDisciplines.Commands.CreateTeacherDiscipline
{
    public class CreateTeacherDisciplineCommand : IRequest<TeacherDisciplineViewModel>
    {
        public int TeacherId { get; set; }
        public int DisciplineId { get; set; }
    }
}
