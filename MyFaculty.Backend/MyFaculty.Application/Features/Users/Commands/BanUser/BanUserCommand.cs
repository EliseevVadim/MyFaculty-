using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Users.Commands.BanUser
{
    public class BanUserCommand : IRequest
    {
        public int BannedUserId { get; set; }
        public int AdministratorId { get; set; }
        public string Reason { get; set; }
    }
}
