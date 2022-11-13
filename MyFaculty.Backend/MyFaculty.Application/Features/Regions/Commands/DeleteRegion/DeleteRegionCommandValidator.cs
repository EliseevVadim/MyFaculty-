using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
