using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPost
{
    public class GetInfoPostQuery : IRequest<InfoPostViewModel>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
    }
}
