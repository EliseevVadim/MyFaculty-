using FluentValidation;

namespace MyFaculty.Application.Features.StudyClubs.Commands.UpdateStudyClub
{
    public class UpdateStudyClubCommandValidator : AbstractValidator<UpdateStudyClubCommand>
    {
        public UpdateStudyClubCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
            RuleFor(command => command.StudyClubName).NotEmpty().MaximumLength(255);
            RuleFor(command => command.OwnerId).NotEmpty();
        }
    }
}
