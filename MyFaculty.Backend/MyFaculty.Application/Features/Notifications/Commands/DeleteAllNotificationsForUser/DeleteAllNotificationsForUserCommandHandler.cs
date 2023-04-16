using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Notifications.Commands.DeleteAllNotificationsForUser
{
    public class DeleteAllNotificationsForUserCommandHandler : IRequestHandler<DeleteAllNotificationsForUserCommand>
    {
        private IMFDbContext _context;

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
