using FluentValidation;

namespace MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPost
{
    public class GetInfoPostQueryValidator : AbstractValidator<GetInfoPostQuery>
    {
        public GetInfoPostQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
            RuleFor(query => query.IssuerId).NotEmpty();
        }
    }
}
