using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Newsfeed.Queries.GeneratePostsNewsfeedForUser
{
    public class GeneratePostsNewsfeedForUserQuery : IRequest<InfoPostsListViewModel>
    {
        public int UserId { get; set; }
    }
}
