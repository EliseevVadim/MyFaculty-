using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Commands.DeleteSecondaryObjectType
{
    public class DeleteSecondaryObjectTypeCommandValidator : AbstractValidator<DeleteSecondaryObjectTypeCommand>
    {
        public DeleteSecondaryObjectTypeCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
