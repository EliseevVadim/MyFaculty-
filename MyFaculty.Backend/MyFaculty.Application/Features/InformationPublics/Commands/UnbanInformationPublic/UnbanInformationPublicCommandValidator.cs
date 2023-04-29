using FluentValidation;

namespace MyFaculty.Application.Features.InformationPublics.Commands.UnbanInformationPublic
{
    public class UnbanInformationPublicCommandValidator : AbstractValidator<UnbanInformationPublicCommand>
    {
        public UnbanInformationPublicCommandValidator()
        {
            RuleFor(command => command.UnbannedPublicId).NotEmpty();
            RuleFor(command => command.AdministratorId).NotEmpty();
            RuleFor(command => command.Reason).NotEmpty();
        }
    }
}
