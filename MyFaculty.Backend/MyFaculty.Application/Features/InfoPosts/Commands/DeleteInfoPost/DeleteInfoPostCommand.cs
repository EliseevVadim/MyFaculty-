using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.InfoPosts.Commands.DeleteInfoPost
{
    public class DeleteInfoPostCommand : IRequest<InfoPostViewModel>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
    }
}
