using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Floors.Commands.UpdateFloor
{
    public class UpdateFloorCommand : IRequest<FloorViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bounds { get; set; }
        public int FacultyId { get; set; }
    }
}
