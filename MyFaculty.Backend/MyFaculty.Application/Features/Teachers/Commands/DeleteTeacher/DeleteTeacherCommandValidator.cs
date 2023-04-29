using FluentValidation;

namespace MyFaculty.Application.Features.Teachers.Commands.DeleteTeacher
{
    public class DeleteTeacherCommandValidator : AbstractValidator<DeleteTeacherCommand>
    {
        public DeleteTeacherCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
