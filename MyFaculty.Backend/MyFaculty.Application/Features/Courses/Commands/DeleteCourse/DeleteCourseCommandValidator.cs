using FluentValidation;

namespace MyFaculty.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
    {
        public DeleteCourseCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
