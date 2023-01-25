using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Commands.DeleteStudyClub
{
    public class DeleteStudyClubCommandValidator : AbstractValidator<DeleteStudyClubCommand>
    {
        public DeleteStudyClubCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
        }
    }
}
