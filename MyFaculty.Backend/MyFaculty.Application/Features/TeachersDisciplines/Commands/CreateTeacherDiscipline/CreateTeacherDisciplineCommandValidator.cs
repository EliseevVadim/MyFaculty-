using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.TeachersDisciplines.Commands.CreateTeacherDiscipline
{
    public class CreateTeacherDisciplineCommandValidator : AbstractValidator<CreateTeacherDisciplineCommand>
    {
        public CreateTeacherDisciplineCommandValidator()
        {
            RuleFor(command => command.TeacherId).NotEmpty();
            RuleFor(command => command.DisciplineId).NotEmpty();
        }
    }
}
