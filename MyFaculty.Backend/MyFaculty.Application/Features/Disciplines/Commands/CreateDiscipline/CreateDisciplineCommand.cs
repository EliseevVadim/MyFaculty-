using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Disciplines.Commands.CreateDiscipline
{
    public class CreateDisciplineCommand : IRequest<DisciplineViewModel>
    {
        public string DisciplineName { get; set; }
    }
}
