using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Courses.Queries.GetCoursesForFaculty
{
    public class GetCoursesForFacultyQuery : IRequest<CoursesListViewModel>
    {
        public int FacultyId { get; set; }
    }
}
