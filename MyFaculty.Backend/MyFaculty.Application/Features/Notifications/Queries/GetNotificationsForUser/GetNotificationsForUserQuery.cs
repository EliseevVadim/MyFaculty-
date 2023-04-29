using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Notifications.Queries.GetNotificationsForUser
{
    public class GetNotificationsForUserQuery : IRequest<NotificationsListViewModel>
    {
        public int UserId { get; set; }
    }
}
