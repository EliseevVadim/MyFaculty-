using FluentValidation;

namespace MyFaculty.Application.Features.Disciplines.Commands.DeleteDiscipline
{
    public class DeleteDisciplineCommandValidator : AbstractValidator<DeleteDisciplineCommand>
    {
        public DeleteDisciplineCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
