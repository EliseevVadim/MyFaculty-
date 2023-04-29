using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriums
{
    public class GetAuditoriumsQuery : IRequest<AuditoriumsListViewModel>
    {

    }
}
