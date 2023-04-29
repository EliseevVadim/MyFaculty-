using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Users.Queries.GetUserInfo
{
    public class GetUserInfoQuery : IRequest<UserViewModel>
    {
        public int Id { get; set; }
    }
}
