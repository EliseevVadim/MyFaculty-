using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Floors.Queries.GetFloorInfo
{
    public class GetFloorInfoQuery : IRequest<FloorViewModel>
    {
        public int Id { get; set; }
    }
}
