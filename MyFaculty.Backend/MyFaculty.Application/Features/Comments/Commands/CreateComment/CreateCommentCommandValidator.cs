using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
