using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Faculties.Commands.CreateFaculty
{
    public class CreateFacultyCommand : IRequest<FacultyViewModel>
    {
        public string FacultyName { get; set; }
        public string OfficialWebsite { get; set; }
    }
}
