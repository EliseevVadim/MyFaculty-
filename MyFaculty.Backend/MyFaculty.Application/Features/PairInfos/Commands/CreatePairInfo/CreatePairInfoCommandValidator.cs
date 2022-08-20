using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.PairInfos.Commands.CreatePairInfo
{
    public class CreatePairInfoCommandValidator : AbstractValidator<CreatePairInfoCommand>
    {
        public CreatePairInfoCommandValidator()
        {
            RuleFor(command => command.PairNumber).NotEmpty();
            RuleFor(command => command.StartTime).NotEmpty().MaximumLength(8);
            RuleFor(command => command.EndTime).NotEmpty().MaximumLength(8);
        }
    }
}
