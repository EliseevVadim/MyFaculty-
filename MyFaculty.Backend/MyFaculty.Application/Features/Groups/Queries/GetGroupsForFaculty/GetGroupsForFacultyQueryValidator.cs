using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Groups.Queries.GetGroupsForFaculty
{
    public class GetGroupsForFacultyQueryValidator : AbstractValidator<GetGroupsForFacultyQuery>
    {
        public GetGroupsForFacultyQueryValidator()
        {
            RuleFor(query => query.FacultyId).NotEmpty();
        }
    }
}
