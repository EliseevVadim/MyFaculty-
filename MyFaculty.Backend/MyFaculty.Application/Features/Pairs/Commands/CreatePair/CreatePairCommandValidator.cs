using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.Pairs.Commands.CreatePair
{
    public class CreatePairCommandValidator : AbstractValidator<CreatePairCommand>
    {
        public CreatePairCommandValidator()
        {
            RuleFor(command => command.PairName).NotEmpty().MaximumLength(250);
            RuleFor(command => command.DayOfWeekId).NotEmpty();
            RuleFor(command => command.TeacherId).NotEmpty();
            RuleFor(command => command.DisciplineId).NotEmpty();
            RuleFor(command => command.PairRepeatingId).NotEmpty();
            RuleFor(command => command.PairInfoId).NotEmpty();
            RuleFor(command => command.GroupId).NotEmpty();
            RuleFor(command => command.AuditoriumId).NotEmpty();
        }
    }
}
