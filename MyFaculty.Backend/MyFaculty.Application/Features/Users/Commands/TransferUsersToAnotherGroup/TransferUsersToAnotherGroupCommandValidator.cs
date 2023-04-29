using FluentValidation;

namespace MyFaculty.Application.Features.Users.Commands.TransferUsersToAnotherGroup
{
    public class TransferUsersToAnotherGroupCommandValidator : AbstractValidator<TransferUsersToAnotherGroupCommand>
    {
        public TransferUsersToAnotherGroupCommandValidator()
        {
            RuleFor(command => command.SourceGroupId).NotEmpty();
            RuleFor(command => command.DestinationGroupId).NotEmpty();
        }
    }
}
