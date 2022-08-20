using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.TeachersDisciplines.Queries.GetTeacherDisciplineInfo
{
    public class GetTeacherDisciplineInfoQueryValidator : AbstractValidator<GetTeacherDisciplineInfoQuery>
    {
        public GetTeacherDisciplineInfoQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty();
        }
    }
}
