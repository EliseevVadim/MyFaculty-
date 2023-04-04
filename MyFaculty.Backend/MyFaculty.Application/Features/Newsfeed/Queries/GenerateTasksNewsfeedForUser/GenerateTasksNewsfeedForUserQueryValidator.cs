using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
