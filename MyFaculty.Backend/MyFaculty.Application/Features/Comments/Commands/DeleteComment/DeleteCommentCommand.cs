using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest<CommentViewModel>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
    }
}
