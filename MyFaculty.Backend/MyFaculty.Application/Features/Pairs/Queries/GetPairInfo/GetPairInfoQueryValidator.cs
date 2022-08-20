using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.Pairs.Queries.GetPairInfo
{
    public class GetPairInfoQueryValidator : AbstractValidator<GetPairInfoQuery>
    {
        public GetPairInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
