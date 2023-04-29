using FluentValidation;

namespace MyFaculty.Application.Features.InformationPublics.Commands.LeaveInformationPublic
{
    public class LeaveInformationPublicCommandValidator : AbstractValidator<LeaveInformationPublicCommand>
    {
        public LeaveInformationPublicCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.PublicId).NotEmpty();
        }
    }
}
