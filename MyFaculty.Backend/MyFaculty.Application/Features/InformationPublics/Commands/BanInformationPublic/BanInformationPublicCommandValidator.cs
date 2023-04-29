using FluentValidation;

namespace MyFaculty.Application.Features.InformationPublics.Commands.BanInformationPublic
{
    public class BanInformationPublicCommandValidator : AbstractValidator<BanInformationPublicCommand>
    {
        public BanInformationPublicCommandValidator()
        {
            RuleFor(command => command.BannedPublicId).NotEmpty();
            RuleFor(command => command.AdministratorId).NotEmpty();
            RuleFor(command => command.Reason).NotEmpty();
        }
    }
}
