using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
