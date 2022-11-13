using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Cities.Queries.GetCitiesForRegion
{
    public class GetCitiesForRegionQueryValidator : AbstractValidator<GetCitiesForRegionQuery>
    {
        public GetCitiesForRegionQueryValidator()
        {
            RuleFor(query => query.RegionId).NotEmpty();
        }
    }
}
