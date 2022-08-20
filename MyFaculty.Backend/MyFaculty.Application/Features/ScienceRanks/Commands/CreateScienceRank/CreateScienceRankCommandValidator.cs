using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.ScienceRanks.Commands.CreateScienceRank
{
    public class CreateScienceRankCommandValidator : AbstractValidator<CreateScienceRankCommand>
    {
        public CreateScienceRankCommandValidator()
        {
            RuleFor(command => command.RankName).NotEmpty().MaximumLength(250);
        }
    }
}
