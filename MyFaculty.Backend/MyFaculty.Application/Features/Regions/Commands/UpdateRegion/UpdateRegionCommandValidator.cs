using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
