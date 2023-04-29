using FluentValidation;

namespace MyFaculty.Application.Features.StudyClubs.Commands.DeleteStudyClub
{
    public class DeleteStudyClubCommandValidator : AbstractValidator<DeleteStudyClubCommand>
    {
        public DeleteStudyClubCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
        }
    }
}
