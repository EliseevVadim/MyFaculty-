using FluentValidation;

namespace MyFaculty.Application.Features.InfoPosts.Commands.UnlikeInfoPost
{
    public class UnlikeInfoPostCommandValidator : AbstractValidator<UnlikeInfoPostCommand>
    {
        public UnlikeInfoPostCommandValidator()
        {
            RuleFor(command => command.LikedPostId).NotEmpty();
            RuleFor(command => command.LikedUserId).NotEmpty();
        }
    }
}
