using FluentValidation;

namespace MyFaculty.Application.Features.Countries.Commands.DeleteCountry
{
    public class DeleteCountryCommandValidator : AbstractValidator<DeleteCountryCommand>
    {
        public DeleteCountryCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
