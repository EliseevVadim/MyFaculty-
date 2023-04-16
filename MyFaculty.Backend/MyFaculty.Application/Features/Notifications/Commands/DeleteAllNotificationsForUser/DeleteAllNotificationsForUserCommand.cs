using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Notifications.Commands.DeleteAllNotificationsForUser
{
    public class DeleteAllNotificationsForUserCommand : IRequest
    {
        public int UserId { get; set; }
    }
}
