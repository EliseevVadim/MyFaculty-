using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommand : IRequest<CourseViewModel>
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public int CourseNumber { get; set; }
        public int FacultyId { get; set; }
    }
}
