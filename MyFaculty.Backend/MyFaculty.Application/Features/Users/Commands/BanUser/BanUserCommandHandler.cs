using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using MyFaculty.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Users.Commands.BanUser
{
    public class BanUserCommandHandler : IRequestHandler<BanUserCommand>
    {
        private IMFDbContext _context;

        public BanUserCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(BanUserCommand request, CancellationToken cancellationToken)
        {
            AppUser bannedUser = await _context.Users.FindAsync(new object[] { request.BannedUserId });
            if (bannedUser == null)
                throw new EntityNotFoundException(nameof(AppUser), request.BannedUserId);
            if (bannedUser.IsBanned)
                throw new DestructiveActionException("Вы не можете заблокировать этого пользователя, поскольку он уже заблокирован.");
            bannedUser.IsBanned = true;
            string query = $"DELETE FROM userroles WHERE userroles.UserId = {request.BannedUserId}";
            UserBanReport banReport = new UserBanReport()
            {
                AffectedUserId = request.BannedUserId,
                AdministratorId = request.AdministratorId,
                Reason = request.Reason,
                PerformedAction = BanAction.Banned,
                Created = DateTime.Now
            };
            await _context.ExecuteSqlRawAsync(query);
            await _context.UsersBansReports.AddAsync(banReport, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
