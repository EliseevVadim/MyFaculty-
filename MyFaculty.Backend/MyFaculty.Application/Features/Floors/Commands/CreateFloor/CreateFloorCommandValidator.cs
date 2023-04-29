using FluentValidation;

namespace MyFaculty.Application.Features.Floors.Commands.CreateFloor
{
    public class CreateFloorCommandValidator : AbstractValidator<CreateFloorCommand>
    {
        public CreateFloorCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().MaximumLength(250);
            RuleFor(command => command.Bounds).NotEmpty();
            RuleFor(command => command.FacultyId).NotEmpty();
        }
    }
}
