using FluentValidation;

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
