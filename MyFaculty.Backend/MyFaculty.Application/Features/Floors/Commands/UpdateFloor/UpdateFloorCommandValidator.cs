using FluentValidation;

namespace MyFaculty.Application.Features.Floors.Commands.UpdateFloor
{
    public class UpdateFloorCommandValidator : AbstractValidator<UpdateFloorCommand>
    {
        public UpdateFloorCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.Name).NotEmpty().MaximumLength(250);
            RuleFor(command => command.Bounds).NotEmpty();
            RuleFor(command => command.FacultyId).NotEmpty();
        }
    }
}
