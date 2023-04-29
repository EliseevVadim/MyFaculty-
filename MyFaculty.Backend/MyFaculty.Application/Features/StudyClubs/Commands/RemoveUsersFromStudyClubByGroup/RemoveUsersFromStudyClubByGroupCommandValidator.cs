using FluentValidation;

namespace MyFaculty.Application.Features.StudyClubs.Commands.RemoveUsersFromStudyClubByGroup
{
    public class RemoveUsersFromStudyClubByGroupCommandValidator : AbstractValidator<RemoveUsersFromStudyClubByGroupCommand>
    {
        public RemoveUsersFromStudyClubByGroupCommandValidator()
        {
            RuleFor(command => command.IssuerId).NotEmpty();
            RuleFor(command => command.GroupId).NotEmpty();
            RuleFor(command => command.StudyClubId).NotEmpty();
        }
    }
}
