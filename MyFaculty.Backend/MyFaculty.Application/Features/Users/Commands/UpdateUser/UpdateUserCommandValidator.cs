using FluentValidation;
using System;

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
            RuleFor(command => command.BirthDate).Must(date => date == null || date <= DateTime.Now);
            RuleFor(command => command.AvatarPath).MaximumLength(250);
        }
    }
}
