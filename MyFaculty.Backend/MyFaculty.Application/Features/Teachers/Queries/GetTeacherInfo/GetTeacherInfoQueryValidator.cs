using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.Teachers.Queries.GetTeacherInfo
{
    public class GetTeacherInfoQueryValidator : AbstractValidator<GetTeacherInfoQuery>
    {
        public GetTeacherInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
