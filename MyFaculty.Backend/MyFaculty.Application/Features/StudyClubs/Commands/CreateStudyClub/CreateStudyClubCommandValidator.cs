using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Commands.CreateStudyClub
{
    public class CreateStudyClubCommandValidator : AbstractValidator<CreateStudyClubCommand>
    {
        public CreateStudyClubCommandValidator()
        {
            RuleFor(command => command.StudyClubName).NotEmpty().MaximumLength(255);
            RuleFor(command => command.OwnerId).NotEmpty();
        }
    }
}
