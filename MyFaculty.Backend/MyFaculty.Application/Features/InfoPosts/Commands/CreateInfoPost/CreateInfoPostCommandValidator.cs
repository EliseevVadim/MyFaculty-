using FluentValidation;

namespace MyFaculty.Application.Features.InfoPosts.Commands.CreateInfoPost
{
    public class CreateInfoPostCommandValidator : AbstractValidator<CreateInfoPostCommand>
    {
        public CreateInfoPostCommandValidator()
        {
            RuleFor(command => command.PostAttachmentsUid).NotEmpty();
            RuleFor(command => command.AuthorId).NotEmpty();
        }
    }
}
