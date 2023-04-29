using FluentValidation;

namespace MyFaculty.Application.Features.Notifications.Commands.DeleteAllNotificationsForUser
{
    public class DeleteAllNotificationsForUserCommandValidator : AbstractValidator<DeleteAllNotificationsForUserCommand>
    {
        public DeleteAllNotificationsForUserCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
        }
    }
}
