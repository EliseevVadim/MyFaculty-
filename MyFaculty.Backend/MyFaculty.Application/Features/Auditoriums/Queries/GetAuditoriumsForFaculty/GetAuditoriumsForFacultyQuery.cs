using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriumsForFaculty
{
    public class GetAuditoriumsForFacultyQuery : IRequest<AuditoriumsListViewModel>
    {
        public int FacultyId { get; set; }
    }
}
