using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.Disciplines.Queries.GetDisciplineInfo
{
    public class GetDisciplineInfoQueryValidator : AbstractValidator<GetDisciplineInfoQuery>
    {
        public GetDisciplineInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
