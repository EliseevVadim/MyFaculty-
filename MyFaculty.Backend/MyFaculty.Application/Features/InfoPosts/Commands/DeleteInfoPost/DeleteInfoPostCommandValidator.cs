using FluentValidation;

namespace MyFaculty.Application.Features.InfoPosts.Commands.DeleteInfoPost
{
    public class DeleteInfoPostCommandValidator : AbstractValidator<DeleteInfoPostCommand>
    {
        public DeleteInfoPostCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
        }
    }
}
