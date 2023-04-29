using FluentValidation;

namespace MyFaculty.Application.Features.Teachers.Queries.GetVerificationTokenQuery
{
    public class GetVerificationTokenQueryValidator : AbstractValidator<GetVerificationTokenQuery>
    {
        public GetVerificationTokenQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
