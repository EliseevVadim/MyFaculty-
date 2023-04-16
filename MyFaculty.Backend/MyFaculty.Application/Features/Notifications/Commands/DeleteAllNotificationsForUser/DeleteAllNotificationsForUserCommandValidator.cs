using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
