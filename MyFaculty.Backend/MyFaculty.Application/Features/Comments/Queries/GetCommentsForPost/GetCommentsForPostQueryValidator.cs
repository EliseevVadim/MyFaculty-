using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
