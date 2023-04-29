using FluentValidation;

namespace MyFaculty.Application.Features.ScienceRanks.Queries.GetScienceRankInfo
{
    public class GetScienceRankInfoQueryValidator : AbstractValidator<GetScienceRankInfoQuery>
    {
        public GetScienceRankInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
