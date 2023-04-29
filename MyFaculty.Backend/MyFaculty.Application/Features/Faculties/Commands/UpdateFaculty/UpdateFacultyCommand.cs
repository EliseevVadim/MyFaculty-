using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Faculties.Commands.UpdateFaculty
{
    public class UpdateFacultyCommand : IRequest<FacultyViewModel>
    {
        public int Id { get; set; }
        public string FacultyName { get; set; }
        public string OfficialWebsite { get; set; }
    }
}
