using FluentValidation;

namespace MyFaculty.Application.Features.Users.Commands.BanUser
{
    public class BanUserCommandValidator : AbstractValidator<BanUserCommand>
    {
        public BanUserCommandValidator()
        {
            RuleFor(command => command.BannedUserId).NotEmpty();
            RuleFor(command => command.AdministratorId).NotEmpty();
            RuleFor(command => command.Reason).NotEmpty();
        }
    }
}
