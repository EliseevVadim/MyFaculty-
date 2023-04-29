using MediatR;

namespace MyFaculty.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommand : IRequest
    {
        public int Id { get; set; }
    }
}
