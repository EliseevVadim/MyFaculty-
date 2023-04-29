using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Courses.Queries.GetCourseInfo
{
    public class GetCourseInfoQuery : IRequest<CourseViewModel>
    {
        public int Id { get; set; }
    }
}
