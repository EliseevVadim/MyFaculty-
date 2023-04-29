using MediatR;

namespace MyFaculty.Application.Features.Disciplines.Commands.DeleteDiscipline
{
    public class DeleteDisciplineCommand : IRequest
    {
        public int Id { get; set; }
    }
}
