using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Faculties.Queries.GetFacultyInfo
{
    public class GetFacultyInfoQuery : IRequest<FacultyViewModel>
    {
        public int Id { get; set; }
    }
}
