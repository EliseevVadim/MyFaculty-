using FluentValidation;

namespace MyFaculty.Application.Features.Regions.Queries.GetRegionsForCountry
{
    public class GetRegionsForCountryQueryValidator : AbstractValidator<GetRegionsForCountryQuery>
    {
        public GetRegionsForCountryQueryValidator()
        {
            RuleFor(query => query.CountryId).NotEmpty();
        }
    }
}
