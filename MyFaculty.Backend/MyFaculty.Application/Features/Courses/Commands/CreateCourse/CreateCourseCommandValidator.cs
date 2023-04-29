using FluentValidation;

namespace MyFaculty.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(command => command.CourseName).NotEmpty().MaximumLength(250);
            RuleFor(command => command.CourseNumber).NotEmpty();
            RuleFor(command => command.FacultyId).NotEmpty();
        }
    }
}
