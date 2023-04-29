using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Groups.Queries.GetGroupInfo
{
    public class GetGroupInfoQuery : IRequest<GroupViewModel>
    {
        public int Id { get; set; }
    }
}
