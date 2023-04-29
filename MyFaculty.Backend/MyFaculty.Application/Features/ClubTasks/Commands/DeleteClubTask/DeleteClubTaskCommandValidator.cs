using FluentValidation;

namespace MyFaculty.Application.Features.ClubTasks.Commands.DeleteClubTask
{
    public class DeleteClubTaskCommandValidator : AbstractValidator<DeleteClubTaskCommand>
    {
        public DeleteClubTaskCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
        }
    }
}
