using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
