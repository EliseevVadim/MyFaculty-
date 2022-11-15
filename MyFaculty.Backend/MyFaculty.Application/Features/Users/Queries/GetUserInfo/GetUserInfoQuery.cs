using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Users.Queries.GetUserInfo
{
    public class GetUserInfoQuery : IRequest<UserViewModel>
    {
        public int Id { get; set; }
    }
}
