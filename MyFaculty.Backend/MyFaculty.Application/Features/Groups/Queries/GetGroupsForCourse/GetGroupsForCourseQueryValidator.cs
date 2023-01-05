using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Groups.Queries.GetGroupsForCourse
{
    public class GetGroupsForCourseQueryValidator : AbstractValidator<GetGroupsForCourseQuery>
    {
        public GetGroupsForCourseQueryValidator()
        {
            RuleFor(query => query.CourseId).NotEmpty();
        }
    }
}
