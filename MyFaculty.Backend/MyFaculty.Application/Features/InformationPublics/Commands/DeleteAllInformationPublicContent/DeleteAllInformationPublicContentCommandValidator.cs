using FluentValidation;

namespace MyFaculty.Application.Features.InformationPublics.Commands.DeleteAllInformationPublicContent
{
    public class DeleteAllInformationPublicContentCommandValidator : AbstractValidator<DeleteAllInformationPublicContentCommand>
    {
        public DeleteAllInformationPublicContentCommandValidator()
        {
            RuleFor(command => command.PublicId).NotEmpty().GreaterThan(1);
        }
    }
}
