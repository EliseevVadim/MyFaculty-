using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriumsForFaculty
{
    public class GetAuditoriumsForFacultyQueryValidator : AbstractValidator<GetAuditoriumsForFacultyQuery>
    {
        public GetAuditoriumsForFacultyQueryValidator()
        {
            RuleFor(query => query.FacultyId).NotEmpty();
        }
    }
}
