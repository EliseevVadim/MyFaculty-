using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Countries.Queries.GetCountryInfo
{
    public class GetCountryInfoQueryValidator : AbstractValidator<GetCountryInfoQuery>
    {
        public GetCountryInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
