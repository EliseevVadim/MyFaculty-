using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Teachers.Queries.GetVerificationTokenQuery
{
    public class GetVerificationTokenQueryValidator : AbstractValidator<GetVerificationTokenQuery>
    {
        public GetVerificationTokenQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
