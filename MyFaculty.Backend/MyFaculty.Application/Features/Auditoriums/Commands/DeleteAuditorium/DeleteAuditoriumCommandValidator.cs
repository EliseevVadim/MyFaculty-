using FluentValidation;

namespace MyFaculty.Application.Features.Auditoriums.Commands.DeleteAuditorium
{
    public class DeleteAuditoriumCommandValidator : AbstractValidator<DeleteAuditoriumCommand>
    {
        public DeleteAuditoriumCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
