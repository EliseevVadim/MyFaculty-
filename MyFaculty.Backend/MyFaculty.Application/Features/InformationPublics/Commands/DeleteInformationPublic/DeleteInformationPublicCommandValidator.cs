using FluentValidation;

namespace MyFaculty.Application.Features.InformationPublics.Commands.DeleteInformationPublic
{
    public class DeleteInformationPublicCommandValidator : AbstractValidator<DeleteInformationPublicCommand>
    {
        public DeleteInformationPublicCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
        }
    }
}
