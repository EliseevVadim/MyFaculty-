using FluentValidation;

namespace MyFaculty.Application.Features.Newsfeed.Queries.GenerateTasksNewsfeedForUser
{
    public class GenerateTasksNewsfeedForUserQueryValidator : AbstractValidator<GenerateTasksNewsfeedForUserQuery>
    {
        public GenerateTasksNewsfeedForUserQueryValidator()
        {
            RuleFor(query => query.UserId).NotEmpty();
        }
    }
}
