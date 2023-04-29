using FluentValidation;

namespace MyFaculty.Application.Features.Pairs.Commands.UpdatePair
{
    public class UpdatePairCommandValidator : AbstractValidator<UpdatePairCommand>
    {
        public UpdatePairCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
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
