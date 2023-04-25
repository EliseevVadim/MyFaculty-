using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Users.Commands.UnbanUser
{
    public class UnbanUserCommandValidator : AbstractValidator<UnbanUserCommand>
    {
        public UnbanUserCommandValidator()
        {
            RuleFor(command => command.UnbannedUserId).NotEmpty();
            RuleFor(command => command.AdministratorId).NotEmpty();
            RuleFor(command => command.Reason).NotEmpty();
        }
    }
}
