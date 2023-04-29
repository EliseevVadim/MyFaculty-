using MediatR;

namespace MyFaculty.Application.Features.Floors.Commands.DeleteFloor
{
    public class DeleteFloorCommand : IRequest
    {
        public int Id { get; set; }
    }
}
