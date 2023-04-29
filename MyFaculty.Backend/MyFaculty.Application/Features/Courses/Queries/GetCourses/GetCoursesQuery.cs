using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Courses.Queries.GetCourses
{
    public class GetCoursesQuery : IRequest<CoursesListViewModel>
    {

    }
}
