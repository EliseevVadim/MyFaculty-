using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.Floors.Commands.DeleteFloor
{
    public class DeleteFloorCommandValidator : AbstractValidator<DeleteFloorCommand>
    {
        public DeleteFloorCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
