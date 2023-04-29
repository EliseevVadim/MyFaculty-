using FluentValidation;

namespace MyFaculty.Application.Features.InformationPublics.Commands.JoinInformationPublic
{
    public class JoinInformationPublicCommandValidator : AbstractValidator<JoinInformationPublicCommand>
    {
        public JoinInformationPublicCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.PublicId).NotEmpty();
        }
    }
}
