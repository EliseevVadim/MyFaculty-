using FluentValidation;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Commands.DeleteSecondaryObjectType
{
    public class DeleteSecondaryObjectTypeCommandValidator : AbstractValidator<DeleteSecondaryObjectTypeCommand>
    {
        public DeleteSecondaryObjectTypeCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
