using FluentValidation;

namespace MyFaculty.Application.Features.ScienceRanks.Commands.UpdateScienceRank
{
    public class UpdateScienceRankCommandValidator : AbstractValidator<UpdateScienceRankCommand>
    {
        public UpdateScienceRankCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.RankName).NotEmpty().MaximumLength(250);
        }
    }
}
