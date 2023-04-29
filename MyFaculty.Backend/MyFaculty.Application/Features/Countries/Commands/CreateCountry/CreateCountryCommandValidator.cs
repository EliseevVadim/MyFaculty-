using FluentValidation;

namespace MyFaculty.Application.Features.Countries.Commands.CreateCountry
{
    public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
    {
        public CreateCountryCommandValidator()
        {
            RuleFor(command => command.CountryName).NotEmpty();
        }
    }
}
