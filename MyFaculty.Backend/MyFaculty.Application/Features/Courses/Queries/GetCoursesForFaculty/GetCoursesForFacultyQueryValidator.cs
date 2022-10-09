using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Courses.Queries.GetCoursesForFaculty
{
    public class GetCoursesForFacultyQueryValidator : AbstractValidator<GetCoursesForFacultyQuery>
    {
        public GetCoursesForFacultyQueryValidator()
        {
            RuleFor(query => query.FacultyId).NotEmpty();
        }
    }
}
