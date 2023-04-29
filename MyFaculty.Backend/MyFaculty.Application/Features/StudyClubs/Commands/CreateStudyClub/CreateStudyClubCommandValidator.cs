using FluentValidation;

namespace MyFaculty.Application.Features.StudyClubs.Commands.CreateStudyClub
{
    public class CreateStudyClubCommandValidator : AbstractValidator<CreateStudyClubCommand>
    {
        public CreateStudyClubCommandValidator()
        {
            RuleFor(command => command.StudyClubName).NotEmpty().MaximumLength(255);
            RuleFor(command => command.OwnerId).NotEmpty();
        }
    }
}
