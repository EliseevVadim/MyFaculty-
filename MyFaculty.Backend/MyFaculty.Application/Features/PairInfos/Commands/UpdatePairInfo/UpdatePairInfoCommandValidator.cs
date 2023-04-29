using FluentValidation;

namespace MyFaculty.Application.Features.PairInfos.Commands.UpdatePairInfo
{
    public class UpdatePairInfoCommandValidator : AbstractValidator<UpdatePairInfoCommand>
    {
        public UpdatePairInfoCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.PairNumber).NotEmpty();
            RuleFor(command => command.StartTime).NotEmpty().MaximumLength(8);
            RuleFor(command => command.EndTime).NotEmpty().MaximumLength(8);
        }
    }
}
