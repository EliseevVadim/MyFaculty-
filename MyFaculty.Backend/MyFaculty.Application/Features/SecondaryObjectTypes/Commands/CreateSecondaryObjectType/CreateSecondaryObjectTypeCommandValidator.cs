using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Commands.CreateSecondaryObjectType
{
    public class CreateSecondaryObjectTypeCommandValidator : AbstractValidator<CreateSecondaryObjectTypeCommand>
    {
        public CreateSecondaryObjectTypeCommandValidator()
        {
            RuleFor(command => command.ObjectTypeName).NotEmpty().MaximumLength(250);
            RuleFor(command => command.TypePath).NotEmpty();
        }
    }
}
