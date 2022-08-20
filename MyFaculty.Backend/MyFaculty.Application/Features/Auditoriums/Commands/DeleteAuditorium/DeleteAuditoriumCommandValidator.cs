using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.Auditoriums.Commands.DeleteAuditorium
{
    public class DeleteAuditoriumCommandValidator : AbstractValidator<DeleteAuditoriumCommand>
    {
        public DeleteAuditoriumCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
