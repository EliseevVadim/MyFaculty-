using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Users.Queries.GetUsersForGroup
{
    public class GetUsersForGroupQuery : IRequest<UsersListViewModel>
    {
        public int GroupId { get; set; }
    }
}
