using MediatR;

namespace MyFaculty.Application.Features.InfoPosts.Commands.UnlikeInfoPost
{
    public class UnlikeInfoPostCommand : IRequest
    {
        public int LikedUserId { get; set; }
        public int LikedPostId { get; set; }
    }
}
