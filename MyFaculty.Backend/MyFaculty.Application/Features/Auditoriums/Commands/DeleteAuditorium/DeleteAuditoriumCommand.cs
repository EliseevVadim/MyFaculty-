using MediatR;

namespace MyFaculty.Application.Features.Auditoriums.Commands.DeleteAuditorium
{
    public class DeleteAuditoriumCommand : IRequest
    {
        public int Id { get; set; }
    }
}
