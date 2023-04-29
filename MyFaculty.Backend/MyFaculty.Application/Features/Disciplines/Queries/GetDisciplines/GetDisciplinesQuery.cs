using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Disciplines.Queries.GetDisciplines
{
    public class GetDisciplinesQuery : IRequest<DisciplinesListViewModel>
    {

    }
}
