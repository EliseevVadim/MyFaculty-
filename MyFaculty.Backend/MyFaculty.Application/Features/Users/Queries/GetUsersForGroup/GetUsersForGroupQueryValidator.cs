using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Users.Queries.GetUsersForGroup
{
    public class GetUsersForGroupQueryValidator : AbstractValidator<GetUsersForGroupQuery>
    {
        public GetUsersForGroupQueryValidator()
        {
            RuleFor(query => query.GroupId).NotEmpty();
        }
    }
}
