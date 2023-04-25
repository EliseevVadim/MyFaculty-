using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Users.Commands.UnbanUser
{
    public class UnbanUserCommand : IRequest<int>
    {
        public int UnbannedUserId { get; set; }
        public int AdministratorId { get; set; }
        public string Reason { get; set; }
    }
}
