using FluentValidation;

namespace MyFaculty.Application.Features.Countries.Queries.GetCountryInfo
{
    public class GetCountryInfoQueryValidator : AbstractValidator<GetCountryInfoQuery>
    {
        public GetCountryInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
