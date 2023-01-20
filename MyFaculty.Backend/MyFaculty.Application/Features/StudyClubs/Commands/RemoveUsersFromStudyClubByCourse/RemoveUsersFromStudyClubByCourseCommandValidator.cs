using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Commands.RemoveUsersFromStudyClubByCourse
{
    public class RemoveUsersFromStudyClubByCourseCommandValidator : AbstractValidator<RemoveUsersFromStudyClubByCourseCommand>
    {
        public RemoveUsersFromStudyClubByCourseCommandValidator()
        {
            RuleFor(command => command.IssuerId).NotEmpty();
            RuleFor(command => command.CourseId).NotEmpty();
            RuleFor(command => command.StudyClubId).NotEmpty();
        }
    }
}
