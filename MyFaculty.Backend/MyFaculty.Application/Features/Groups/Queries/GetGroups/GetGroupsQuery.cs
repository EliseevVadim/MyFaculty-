using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Groups.Queries.GetGroups
{
    public class GetGroupsQuery : IRequest<GroupsListViewModel>
    {

    }
}
