using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPost
{
    public class GetInfoPostQueryValidator : AbstractValidator<GetInfoPostQuery>
    {
        public GetInfoPostQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
