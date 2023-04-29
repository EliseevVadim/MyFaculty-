using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Floors.Queries.GetFloorsForFaculty
{
    public class GetFloorsForFacultyQuery : IRequest<FloorsListViewModel>
    {
        public int FacultyId { get; set; }
    }
}
