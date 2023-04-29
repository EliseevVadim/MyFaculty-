using FluentValidation;

namespace MyFaculty.Application.Features.Groups.Commands.DeleteGroup
{
    public class DeleteGroupCommandValidator : AbstractValidator<DeleteGroupCommand>
    {
        public DeleteGroupCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
