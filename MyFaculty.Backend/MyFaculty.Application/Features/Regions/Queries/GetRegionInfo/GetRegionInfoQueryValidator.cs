using FluentValidation;

namespace MyFaculty.Application.Features.Regions.Queries.GetRegionInfo
{
    public class GetRegionInfoQueryValidator : AbstractValidator<GetRegionInfoQuery>
    {
        public GetRegionInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
