using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Floors.Commands.CreateFloor
{
    public class CreateFloorCommand : IRequest<FloorViewModel>
    {
        public string Name { get; set; }
        public string Bounds { get; set; }
        public int FacultyId { get; set; }
    }
}
