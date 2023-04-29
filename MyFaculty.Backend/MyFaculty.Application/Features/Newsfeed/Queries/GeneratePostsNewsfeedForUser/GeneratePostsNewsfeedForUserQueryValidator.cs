using FluentValidation;

namespace MyFaculty.Application.Features.Newsfeed.Queries.GeneratePostsNewsfeedForUser
{
    public class GeneratePostsNewsfeedForUserQueryValidator : AbstractValidator<GeneratePostsNewsfeedForUserQuery>
    {
        public GeneratePostsNewsfeedForUserQueryValidator()
        {
            RuleFor(query => query.UserId).NotEmpty();
        }
    }
}
