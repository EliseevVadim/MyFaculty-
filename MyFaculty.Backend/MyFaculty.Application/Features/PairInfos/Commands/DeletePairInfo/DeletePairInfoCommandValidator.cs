using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.PairInfos.Commands.DeletePairInfo
{
    public class DeletePairInfoCommandValidator : AbstractValidator<DeletePairInfoCommand>
    {
        public DeletePairInfoCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
