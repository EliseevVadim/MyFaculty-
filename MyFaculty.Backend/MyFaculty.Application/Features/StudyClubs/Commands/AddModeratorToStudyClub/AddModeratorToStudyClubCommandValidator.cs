using FluentValidation;

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
