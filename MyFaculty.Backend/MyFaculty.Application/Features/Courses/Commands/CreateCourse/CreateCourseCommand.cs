using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommand : IRequest<CourseViewModel>
    {
        public string CourseName { get; set; }
        public int CourseNumber { get; set; }
        public int FacultyId { get; set; }
    }
}
