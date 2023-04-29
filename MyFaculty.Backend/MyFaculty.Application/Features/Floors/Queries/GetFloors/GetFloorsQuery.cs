using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Floors.Queries.GetFloors
{
    public class GetFloorsQuery : IRequest<FloorsListViewModel>
    {
    }
}
