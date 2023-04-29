using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.InfoPosts.Commands.UpdateInfoPost
{
    public class UpdateInfoPostCommand : IRequest<InfoPostViewModel>
    {
        public int InfoPostId { get; set; }
        public int IssuerId { get; set; }
        public string TextContent { get; set; }
        public string Attachments { get; set; }
        public bool CommentsAllowed { get; set; }
    }
}
