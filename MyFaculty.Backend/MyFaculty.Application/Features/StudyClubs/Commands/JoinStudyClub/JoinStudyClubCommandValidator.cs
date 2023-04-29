using FluentValidation;

namespace MyFaculty.Application.Features.StudyClubs.Commands.JoinStudyClub
{
    public class JoinStudyClubCommandValidator : AbstractValidator<JoinStudyClubCommand>
    {
        public JoinStudyClubCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.StudyClubId).NotEmpty();
        }
    }
}
