using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.Floors.Queries.GetFloorInfo
{
    public class GetFloorInfoQueryValidator : AbstractValidator<GetFloorInfoQuery>
    {
        public GetFloorInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
