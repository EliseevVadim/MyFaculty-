using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.Groups.Queries.GetGroupInfo
{
    public class GetGroupInfoQueryValidator : AbstractValidator<GetGroupInfoQuery>
    {
        public GetGroupInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
