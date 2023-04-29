using FluentValidation;

namespace MyFaculty.Application.Features.StudyClubs.Commands.RemoveUserFromStudyClub
{
    public class RemoveUserFromStudyClubCommandValidator : AbstractValidator<RemoveUserFromStudyClubCommand>
    {
        public RemoveUserFromStudyClubCommandValidator()
        {
            RuleFor(command => command.IssuerId).NotEmpty();
            RuleFor(command => command.RemovingUserId).NotEmpty();
            RuleFor(command => command.StudyClubId).NotEmpty();
        }
    }
}
