using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Disciplines.Commands.UpdateDiscipline
{
    public class UpdateDisciplineCommand : IRequest<DisciplineViewModel>
    {
        public int Id { get; set; }
        public string DisciplineName { get; set; }
    }
}
