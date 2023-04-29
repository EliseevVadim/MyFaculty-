using FluentValidation;

namespace MyFaculty.Application.Features.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherCommandValidator : AbstractValidator<UpdateTeacherCommand>
    {
        public UpdateTeacherCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.FIO).NotEmpty();
            RuleFor(command => command.PhotoPath).MaximumLength(250);
            RuleFor(command => command.BirthDate).NotEmpty();
            RuleFor(command => command.Email).NotEmpty().EmailAddress();
            RuleFor(command => command.ScienceRankId).NotEmpty();
        }
    }
}
