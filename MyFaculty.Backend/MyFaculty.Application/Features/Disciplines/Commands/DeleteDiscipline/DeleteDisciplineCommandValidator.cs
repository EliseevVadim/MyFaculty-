using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.Disciplines.Commands.DeleteDiscipline
{
    public class DeleteDisciplineCommandValidator : AbstractValidator<DeleteDisciplineCommand>
    {
        public DeleteDisciplineCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
