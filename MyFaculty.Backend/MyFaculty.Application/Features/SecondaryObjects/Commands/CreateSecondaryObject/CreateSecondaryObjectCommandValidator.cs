using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.SecondaryObjects.Commands.CreateSecondaryObject
{
    public class CreateSecondaryObjectCommandValidator : AbstractValidator<CreateSecondaryObjectCommand>
    {
        public CreateSecondaryObjectCommandValidator()
        {
            RuleFor(command => command.ObjectName).NotEmpty().MaximumLength(250);
            RuleFor(command => command.PositionInfo).NotEmpty();
            RuleFor(command => command.FloorId).NotEmpty();
            RuleFor(command => command.SecondaryObjectTypeId).NotEmpty();
        }
    }
}
