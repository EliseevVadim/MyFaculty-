using FluentValidation;

namespace MyFaculty.Application.Features.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherCommandValidator : AbstractValidator<CreateTeacherCommand>
    {
        public CreateTeacherCommandValidator()
        {
            RuleFor(command => command.FIO).NotEmpty();
            RuleFor(command => command.PhotoPath).MaximumLength(250);
            RuleFor(command => command.BirthDate).NotEmpty();
            RuleFor(command => command.Email).NotEmpty().EmailAddress();
            RuleFor(command => command.ScienceRankId).NotEmpty();
        }
    }
}
