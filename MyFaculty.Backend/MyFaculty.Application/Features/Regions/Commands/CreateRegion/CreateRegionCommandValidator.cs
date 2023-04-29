using FluentValidation;

namespace MyFaculty.Application.Features.Regions.Commands.CreateRegion
{
    public class CreateRegionCommandValidator : AbstractValidator<CreateRegionCommand>
    {
        public CreateRegionCommandValidator()
        {
            RuleFor(command => command.RegionName).NotEmpty();
            RuleFor(command => command.CountryId).NotEmpty();
        }
    }
}
