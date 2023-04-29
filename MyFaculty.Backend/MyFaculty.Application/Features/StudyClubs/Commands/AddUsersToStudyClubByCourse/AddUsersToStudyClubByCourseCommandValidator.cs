using FluentValidation;

namespace MyFaculty.Application.Features.StudyClubs.Commands.AddUsersToStudyClubByCourse
{
    public class AddUsersToStudyClubByCourseCommandValidator : AbstractValidator<AddUsersToStudyClubByCourseCommand>
    {
        public AddUsersToStudyClubByCourseCommandValidator()
        {
            RuleFor(command => command.IssuerId).NotEmpty();
            RuleFor(command => command.CourseId).NotEmpty();
            RuleFor(command => command.StudyClubId).NotEmpty();
        }
    }
}
