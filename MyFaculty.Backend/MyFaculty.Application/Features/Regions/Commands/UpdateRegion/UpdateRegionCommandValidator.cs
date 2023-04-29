using FluentValidation;

namespace MyFaculty.Application.Features.Regions.Commands.UpdateRegion
{
    public class UpdateRegionCommandValidator : AbstractValidator<UpdateRegionCommand>
    {
        public UpdateRegionCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.RegionName).NotEmpty();
            RuleFor(command => command.CountryId).NotEmpty();
        }
    }
}
