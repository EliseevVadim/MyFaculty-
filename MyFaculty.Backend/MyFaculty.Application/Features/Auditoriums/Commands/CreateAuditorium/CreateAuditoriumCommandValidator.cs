using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.Auditoriums.Commands.CreateAuditorium
{
    public class CreateAuditoriumCommandValidator : AbstractValidator<CreateAuditoriumCommand>
    {
        public CreateAuditoriumCommandValidator()
        {
            RuleFor(command => command.AuditoriumName).NotEmpty().MaximumLength(250);
            RuleFor(command => command.PositionInfo).NotEmpty();
            RuleFor(command => command.TeacherId).NotEmpty();
            RuleFor(command => command.FloorId).NotEmpty();
        }
    }
}
