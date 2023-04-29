using MediatR;
using MyFaculty.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Notifications.Commands.DeleteAllNotificationsForUser
{
    public class DeleteAllNotificationsForUserCommandHandler : IRequestHandler<DeleteAllNotificationsForUserCommand>
    {
        private readonly IMFDbContext _context;

        public DeleteAllNotificationsForUserCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteAllNotificationsForUserCommand request, CancellationToken cancellationToken)
        {
            var notifications = _context.Notifications
                .Where(notification => notification.UserId == request.UserId);
            _context.Notifications.RemoveRange(notifications);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
