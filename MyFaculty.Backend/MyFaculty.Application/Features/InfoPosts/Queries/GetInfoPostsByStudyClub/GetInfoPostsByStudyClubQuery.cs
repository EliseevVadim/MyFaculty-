using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPostsByStudyClub
{
    public class GetInfoPostsByStudyClubQuery : IRequest<InfoPostsListViewModel>
    {
        public int StudyClubId { get; set; }
    }
}
