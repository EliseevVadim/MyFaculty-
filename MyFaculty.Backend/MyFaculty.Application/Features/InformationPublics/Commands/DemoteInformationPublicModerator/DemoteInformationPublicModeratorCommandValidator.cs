using FluentValidation;

namespace MyFaculty.Application.Features.InformationPublics.Commands.DemoteInformationPublicModerator
{
    public class DemoteInformationPublicModeratorCommandValidator : AbstractValidator<DemoteInformationPublicModeratorCommand>
    {
        public DemoteInformationPublicModeratorCommandValidator()
        {
            RuleFor(command => command.IssuerId).NotEmpty();
            RuleFor(command => command.InformationPublicId).NotEmpty();
            RuleFor(command => command.ModeratorId).NotEmpty();
        }
    }
}
