using FluentValidation;

namespace MyFaculty.Application.Features.InformationPublics.Commands.UnblockUserAtInformationPublic
{
    public class UnblockUserAtInformationPublicCommandValidator : AbstractValidator<UnblockUserAtInformationPublicCommand>
    {
        public UnblockUserAtInformationPublicCommandValidator()
        {
            RuleFor(command => command.PublicId).NotEmpty();
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
        }
    }
}
