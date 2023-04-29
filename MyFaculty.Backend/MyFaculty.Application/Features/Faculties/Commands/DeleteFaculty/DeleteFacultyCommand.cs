using MediatR;

namespace MyFaculty.Application.Features.Faculties.Commands.DeleteFaculty
{
    public class DeleteFacultyCommand : IRequest
    {
        public int Id { get; set; }
    }
}
