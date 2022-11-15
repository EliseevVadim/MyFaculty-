using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Regions.Queries.GetRegionInfo
{
    public class GetRegionInfoQueryValidator : AbstractValidator<GetRegionInfoQuery>
    {
        public GetRegionInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
