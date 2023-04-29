using FluentValidation;

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
