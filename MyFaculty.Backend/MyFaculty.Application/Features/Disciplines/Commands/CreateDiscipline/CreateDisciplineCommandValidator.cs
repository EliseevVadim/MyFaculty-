using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.Disciplines.Commands.CreateDiscipline
{
    public class CreateDisciplineCommandValidator : AbstractValidator<CreateDisciplineCommand>
    {
        public CreateDisciplineCommandValidator()
        {
            RuleFor(command => command.DisciplineName).NotEmpty().MaximumLength(250);
        }
    }
}
