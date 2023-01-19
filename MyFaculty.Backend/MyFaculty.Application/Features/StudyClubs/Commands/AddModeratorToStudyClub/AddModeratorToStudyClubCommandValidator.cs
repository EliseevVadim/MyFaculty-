using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Commands.AddModeratorToStudyClub
{
    public class AddModeratorToStudyClubCommandValidator : AbstractValidator<AddModeratorToStudyClubCommand>
    {
        public AddModeratorToStudyClubCommandValidator()
        {
            RuleFor(command => command.IssuerId).NotEmpty();
            RuleFor(command => command.StudyClubId).NotEmpty();
            RuleFor(command => command.ModeratorId).NotEmpty();
        }
    }
}
