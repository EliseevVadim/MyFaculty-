using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Commands.AddUsersToStudyClubByGroup
{
    public class AddUsersToStudyClubByGroupCommandValidator : AbstractValidator<AddUsersToStudyClubByGroupCommand>
    {
        public AddUsersToStudyClubByGroupCommandValidator()
        {
            RuleFor(command => command.IssuerId).NotEmpty();
            RuleFor(command => command.GroupId).NotEmpty();
            RuleFor(command => command.StudyClubId).NotEmpty();
        }
    }
}
