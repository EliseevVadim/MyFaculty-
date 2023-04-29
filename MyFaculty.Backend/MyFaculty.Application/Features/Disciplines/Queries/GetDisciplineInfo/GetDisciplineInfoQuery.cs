using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Disciplines.Queries.GetDisciplineInfo
{
    public class GetDisciplineInfoQuery : IRequest<DisciplineViewModel>
    {
        public int Id { get; set; }
    }
}
