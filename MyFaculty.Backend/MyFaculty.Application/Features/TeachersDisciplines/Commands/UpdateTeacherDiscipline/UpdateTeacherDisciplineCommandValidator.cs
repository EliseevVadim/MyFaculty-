using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.TeachersDisciplines.Commands.UpdateTeacherDiscipline
{
    public class UpdateTeacherDisciplineCommandValidator : AbstractValidator<UpdateTeacherDisciplineCommand>
    {
        public UpdateTeacherDisciplineCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.TeacherId).NotEmpty();
            RuleFor(command => command.DisciplineId).NotEmpty();
        }
    }
}
