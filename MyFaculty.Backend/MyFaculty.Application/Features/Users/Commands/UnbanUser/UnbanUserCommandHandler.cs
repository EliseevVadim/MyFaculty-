using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using MyFaculty.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Users.Commands.UnbanUser
{
    public class UnbanUserCommandHandler : IRequestHandler<UnbanUserCommand, int>
    {
        private IMFDbContext _context;

        public UnbanUserCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UnbanUserCommand request, CancellationToken cancellationToken)
        {
            AppUser unbannedUser = await _context.Users.FindAsync(new object[] { request.UnbannedUserId });
            if (unbannedUser == null)
                throw new EntityNotFoundException(nameof(AppUser), request.UnbannedUserId);
            if (!unbannedUser.IsBanned)
                throw new DestructiveActionException("Вы не можете разблокировать этого пользователя, поскольку он не заблокирован.");
            unbannedUser.IsBanned = false;
            UserBanReport unbanReport = new UserBanReport()
            {
                AffectedUserId = request.UnbannedUserId,
                AdministratorId = request.AdministratorId,
                Reason = request.Reason,
                PerformedAction = BanAction.Unbanned,
                Created = DateTime.Now
            };
            await _context.UsersBansReports.AddAsync(unbanReport, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return unbannedUser.Id;
        }
    }
}
