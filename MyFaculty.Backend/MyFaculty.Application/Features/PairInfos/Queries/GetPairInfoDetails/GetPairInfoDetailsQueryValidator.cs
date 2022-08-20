using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.PairInfos.Queries.GetPairInfoDetails
{
    public class GetPairInfoDetailsQueryValidator : AbstractValidator<GetPairInfoDetailsQuery>
    {
        public GetPairInfoDetailsQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
