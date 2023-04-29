using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<UsersListViewModel>
    {

    }
}
