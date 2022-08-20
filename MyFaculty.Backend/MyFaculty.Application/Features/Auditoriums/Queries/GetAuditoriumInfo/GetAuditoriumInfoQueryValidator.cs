using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriumInfo
{
    public class GetAuditoriumInfoQueryValidator : AbstractValidator<GetAuditoriumInfoQuery>
    {
        public GetAuditoriumInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
