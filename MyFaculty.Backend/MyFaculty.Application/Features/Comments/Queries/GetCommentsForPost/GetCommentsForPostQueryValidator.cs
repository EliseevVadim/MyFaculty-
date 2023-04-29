using FluentValidation;

namespace MyFaculty.Application.Features.Comments.Queries.GetCommentsForPost
{
    public class GetCommentsForPostQueryValidator : AbstractValidator<GetCommentsForPostQuery>
    {
        public GetCommentsForPostQueryValidator()
        {
            RuleFor(query => query.PostId).NotEmpty();
            RuleFor(query => query.IssuerId).NotEmpty();
        }
    }
}
