using FluentValidation;

namespace MyFaculty.Application.Features.Cities.Queries.GetCityInfo
{
    public class GetCityInfoQueryValidator : AbstractValidator<GetCityInfoQuery>
    {
        public GetCityInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
