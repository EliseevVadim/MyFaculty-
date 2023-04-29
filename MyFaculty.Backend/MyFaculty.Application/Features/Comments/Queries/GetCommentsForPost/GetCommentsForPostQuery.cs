using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Comments.Queries.GetCommentsForPost
{
    public class GetCommentsForPostQuery : IRequest<CommentsListViewModel>
    {
        public int PostId { get; set; }
        public int IssuerId { get; set; }
    }
}
