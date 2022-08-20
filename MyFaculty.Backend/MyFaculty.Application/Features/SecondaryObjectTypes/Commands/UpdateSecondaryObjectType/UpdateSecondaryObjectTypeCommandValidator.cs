using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Commands.UpdateSecondaryObjectType
{
    public class UpdateSecondaryObjectTypeCommandValidator : AbstractValidator<UpdateSecondaryObjectTypeCommand>
    {
        public UpdateSecondaryObjectTypeCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.ObjectTypeName).NotEmpty().MaximumLength(250);
            RuleFor(command => command.TypePath).NotEmpty();
        }
    }
}
