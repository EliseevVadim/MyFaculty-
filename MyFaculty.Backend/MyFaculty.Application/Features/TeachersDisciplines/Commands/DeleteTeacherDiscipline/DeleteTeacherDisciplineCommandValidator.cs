using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.TeachersDisciplines.Commands.DeleteTeacherDiscipline
{
    public class DeleteTeacherDisciplineCommandValidator : AbstractValidator<DeleteTeacherDisciplineCommand>
    {
        public DeleteTeacherDisciplineCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
