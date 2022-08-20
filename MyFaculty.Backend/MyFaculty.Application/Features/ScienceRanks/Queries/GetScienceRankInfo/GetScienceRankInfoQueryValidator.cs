using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.ScienceRanks.Queries.GetScienceRankInfo
{
    public class GetScienceRankInfoQueryValidator : AbstractValidator<GetScienceRankInfoQuery>
    {
        public GetScienceRankInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
