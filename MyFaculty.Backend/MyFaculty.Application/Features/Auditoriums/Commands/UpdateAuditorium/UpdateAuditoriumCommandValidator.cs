using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.Auditoriums.Commands.UpdateAuditorium
{
    public class UpdateAuditoriumCommandValidator : AbstractValidator<UpdateAuditoriumCommand>
    {
        public UpdateAuditoriumCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.AuditoriumName).NotEmpty().MaximumLength(250);
            RuleFor(command => command.PositionInfo).NotEmpty();
            RuleFor(command => command.TeacherId).NotEmpty();
            RuleFor(command => command.FloorId).NotEmpty();
        }
    }
}
