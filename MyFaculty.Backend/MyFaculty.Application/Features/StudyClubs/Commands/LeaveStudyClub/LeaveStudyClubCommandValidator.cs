using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Commands.LeaveStudyClub
{
    public class LeaveStudyClubCommandValidator : AbstractValidator<LeaveStudyClubCommand>
    {
        public LeaveStudyClubCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.StudyClubId).NotEmpty();
        }
    }
}
