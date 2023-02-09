using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InfoPosts.Commands.UnlikeInfoPost
{
    public class UnlikeInfoPostCommand : IRequest
    {
        public int LikedUserId { get; set; }
        public int LikedPostId { get; set; }
    }
}
