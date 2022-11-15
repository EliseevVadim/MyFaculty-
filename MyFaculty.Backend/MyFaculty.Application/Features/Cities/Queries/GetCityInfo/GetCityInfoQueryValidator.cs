using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Cities.Queries.GetCityInfo
{
    public class GetCityInfoQueryValidator : AbstractValidator<GetCityInfoQuery>
    {
        public GetCityInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
