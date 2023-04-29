using FluentValidation;

namespace MyFaculty.Application.Features.Pairs.Queries.GetPairInfo
{
    public class GetPairInfoQueryValidator : AbstractValidator<GetPairInfoQuery>
    {
        public GetPairInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
