using FluentValidation;

namespace MyFaculty.Application.Features.SecondaryObjects.Commands.DeleteSecondaryObject
{
    public class DeleteSecondaryObjectCommandValidator : AbstractValidator<DeleteSecondaryObjectCommand>
    {
        public DeleteSecondaryObjectCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
