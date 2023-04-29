using FluentValidation;

namespace MyFaculty.Application.Features.InformationPublics.Commands.BlockUserAtInformationPublic
{
    public class BlockUserAtInformationPublicCommandValidator : AbstractValidator<BlockUserAtInformationPublicCommand>
    {
        public BlockUserAtInformationPublicCommandValidator()
        {
            RuleFor(command => command.PublicId).NotEmpty();
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
        }
    }
}
