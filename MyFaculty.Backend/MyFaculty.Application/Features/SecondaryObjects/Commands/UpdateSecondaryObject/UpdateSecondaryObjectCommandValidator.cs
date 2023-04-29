using FluentValidation;

namespace MyFaculty.Application.Features.SecondaryObjects.Commands.UpdateSecondaryObject
{
    public class UpdateSecondaryObjectCommandValidator : AbstractValidator<UpdateSecondaryObjectCommand>
    {
        public UpdateSecondaryObjectCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.ObjectName).NotEmpty().MaximumLength(250);
            RuleFor(command => command.PositionInfo).NotEmpty();
            RuleFor(command => command.FloorId).NotEmpty();
            RuleFor(command => command.SecondaryObjectTypeId).NotEmpty();
        }
    }
}
