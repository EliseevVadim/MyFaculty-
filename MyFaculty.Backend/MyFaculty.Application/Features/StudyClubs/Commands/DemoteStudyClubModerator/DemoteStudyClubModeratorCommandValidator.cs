using FluentValidation;

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
