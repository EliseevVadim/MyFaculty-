using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Newsfeed.Queries.GenerateTasksNewsfeedForUser
{
    public class GenerateTasksNewsfeedForUserQuery : IRequest<ClubTasksListViewModel>
    {
        public int UserId { get; set; }
    }
}
