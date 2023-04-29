using FluentValidation;

namespace MyFaculty.Application.Features.PairInfos.Queries.GetPairInfoDetails
{
    public class GetPairInfoDetailsQueryValidator : AbstractValidator<GetPairInfoDetailsQuery>
    {
        public GetPairInfoDetailsQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
