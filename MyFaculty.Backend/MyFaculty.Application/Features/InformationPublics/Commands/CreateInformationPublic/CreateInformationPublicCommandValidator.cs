using FluentValidation;

namespace MyFaculty.Application.Features.InformationPublics.Commands.CreateInformationPublic
{
    public class CreateInformationPublicCommandValidator : AbstractValidator<CreateInformationPublicCommand>
    {
        public CreateInformationPublicCommandValidator()
        {
            RuleFor(command => command.PublicName).NotEmpty().MaximumLength(255);
            RuleFor(command => command.OwnerId).NotEmpty();
        }
    }
}
