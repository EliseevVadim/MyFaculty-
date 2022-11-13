using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Regions.Queries.GetRegionsForCountry
{
    public class GetRegionsForCountryQueryValidator : AbstractValidator<GetRegionsForCountryQuery>
    {
        public GetRegionsForCountryQueryValidator()
        {
            RuleFor(query => query.CountryId).NotEmpty();
        }
    }
}
