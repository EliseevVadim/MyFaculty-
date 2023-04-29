using FluentValidation;

namespace MyFaculty.Application.Features.Disciplines.Commands.UpdateDiscipline
{
    public class UpdateDisciplineCommandValidator : AbstractValidator<UpdateDisciplineCommand>
    {
        public UpdateDisciplineCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.DisciplineName).NotEmpty().MaximumLength(250);
        }
    }
}
