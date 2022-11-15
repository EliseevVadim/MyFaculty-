using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.Email).NotEmpty().EmailAddress();
            RuleFor(command => command.FirstName).NotEmpty();
            RuleFor(command => command.LastName).NotEmpty();
            RuleFor(command => command.AvatarPath).NotEmpty().MaximumLength(250);
        }
    }
}
