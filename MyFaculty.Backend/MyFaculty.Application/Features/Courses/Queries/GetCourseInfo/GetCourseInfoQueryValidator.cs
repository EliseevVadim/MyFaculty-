using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.Courses.Queries.GetCourseInfo
{
    public class GetCourseInfoQueryValidator : AbstractValidator<GetCourseInfoQuery>
    {
        public GetCourseInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
