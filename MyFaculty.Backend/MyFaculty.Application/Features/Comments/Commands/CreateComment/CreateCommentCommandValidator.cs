using FluentValidation;

namespace MyFaculty.Application.Features.Comments.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(comment => comment.CommentAttachmentsUid).NotEmpty();
            RuleFor(comment => comment.AuthorId).NotEmpty();
            RuleFor(comment => comment.PostId).NotEmpty();
        }
    }
}
