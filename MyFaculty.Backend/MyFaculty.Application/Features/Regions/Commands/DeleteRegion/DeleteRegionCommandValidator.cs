using FluentValidation;

namespace MyFaculty.Application.Features.Regions.Commands.DeleteRegion
{
    public class DeleteRegionCommandValidator : AbstractValidator<DeleteRegionCommand>
    {
        public DeleteRegionCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
