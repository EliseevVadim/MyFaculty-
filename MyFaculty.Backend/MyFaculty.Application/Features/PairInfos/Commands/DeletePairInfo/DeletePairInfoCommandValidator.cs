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
