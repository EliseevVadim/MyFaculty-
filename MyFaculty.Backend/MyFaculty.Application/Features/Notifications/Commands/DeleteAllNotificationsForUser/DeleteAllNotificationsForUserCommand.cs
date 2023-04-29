using MediatR;

namespace MyFaculty.Application.Features.Notifications.Commands.DeleteAllNotificationsForUser
{
    public class DeleteAllNotificationsForUserCommand : IRequest
    {
        public int UserId { get; set; }
    }
}
