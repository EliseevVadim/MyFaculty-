using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Faculties.Queries.GetFaculties
{
    public class GetFacultiesQuery : IRequest<FacultiesListViewModel>
    {

    }
}
