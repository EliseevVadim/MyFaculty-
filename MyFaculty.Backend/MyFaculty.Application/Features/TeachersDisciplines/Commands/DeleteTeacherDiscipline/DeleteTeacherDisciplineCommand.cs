using MediatR;

namespace MyFaculty.Application.Features.TeachersDisciplines.Commands.DeleteTeacherDiscipline
{
    public class DeleteTeacherDisciplineCommand : IRequest
    {
        public int Id { get; set; }
    }
}
