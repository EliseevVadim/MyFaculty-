using FluentValidation;

namespace MyFaculty.Application.Features.InformationPublics.Commands.UpdateInformationPublic
{
    public class UpdateInformationPublicCommandValidator : AbstractValidator<UpdateInformationPublicCommand>
    {
        public UpdateInformationPublicCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
            RuleFor(command => command.PublicName).NotEmpty().MaximumLength(255);
            RuleFor(command => command.OwnerId).NotEmpty();
        }
    }
}
