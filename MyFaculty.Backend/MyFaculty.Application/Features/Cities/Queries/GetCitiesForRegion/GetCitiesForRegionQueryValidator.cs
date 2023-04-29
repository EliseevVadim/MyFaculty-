using FluentValidation;

namespace MyFaculty.Application.Features.Cities.Queries.GetCitiesForRegion
{
    public class GetCitiesForRegionQueryValidator : AbstractValidator<GetCitiesForRegionQuery>
    {
        public GetCitiesForRegionQueryValidator()
        {
            RuleFor(query => query.RegionId).NotEmpty();
        }
    }
}
