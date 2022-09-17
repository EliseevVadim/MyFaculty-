using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Faculties.Queries.GetFacultyInfo
{
    public class GetFacultyInfoQueryValidator : AbstractValidator<GetFacultyInfoQuery>
    {
        public GetFacultyInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
