using MediatR;

namespace MyFaculty.Application.Features.InfoPosts.Commands.LikeInfoPost
{
    public class LikeInfoPostCommand : IRequest
    {
        public int LikedUserId { get; set; }
        public int LikedPostId { get; set; }
    }
}
