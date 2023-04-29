using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPostsByInformationPublic
{
    public class GetInfoPostsByInformationPublicQuery : IRequest<InfoPostsListViewModel>
    {
        public int PublicId { get; set; }
        public int IssuerId { get; set; }
    }
}
