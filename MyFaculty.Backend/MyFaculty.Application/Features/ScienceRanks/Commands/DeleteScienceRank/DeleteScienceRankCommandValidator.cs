using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.ScienceRanks.Commands.DeleteScienceRank
{
    public class DeleteScienceRankCommandValidator : AbstractValidator<DeleteScienceRankCommand>
    {
        public DeleteScienceRankCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }
}
