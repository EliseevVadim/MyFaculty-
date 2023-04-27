using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.DemoteInformationPublicModerator
{
    public class DemoteInformationPublicModeratorCommandValidator : AbstractValidator<DemoteInformationPublicModeratorCommand>
    {
        public DemoteInformationPublicModeratorCommandValidator()
        {
            RuleFor(command => command.IssuerId).NotEmpty();
            RuleFor(command => command.InformationPublicId).NotEmpty();
            RuleFor(command => command.ModeratorId).NotEmpty();
        }
    }
}
