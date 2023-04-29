using FluentValidation;

namespace MyFaculty.Application.Features.InfoPosts.Commands.UpdateInfoPost
{
    public class UpdateInfoPostCommandValidator : AbstractValidator<UpdateInfoPostCommand>
    {
        public UpdateInfoPostCommandValidator()
        {
            RuleFor(command => command.InfoPostId).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
        }
    }
}
