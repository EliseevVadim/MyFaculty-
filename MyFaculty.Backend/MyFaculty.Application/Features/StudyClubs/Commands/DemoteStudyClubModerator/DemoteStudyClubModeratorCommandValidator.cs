using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Commands.DemoteStudyClubModerator
{
    public class DemoteStudyClubModeratorCommandValidator : AbstractValidator<DemoteStudyClubModeratorCommand>
    {
        public DemoteStudyClubModeratorCommandValidator()
        {
            RuleFor(command => command.IssuerId).NotEmpty();
            RuleFor(command => command.StudyClubId).NotEmpty();
            RuleFor(command => command.ModeratorId).NotEmpty();
        }
    }
}
