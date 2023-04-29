using FluentValidation;

namespace MyFaculty.Application.Features.Teachers.Commands.VerifyTeacher
{
    public class VerifyTeacherCommandValidator : AbstractValidator<VerifyTeacherCommand>
    {
        public VerifyTeacherCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.VerificationToken).NotEmpty();
        }
    }
}
