using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.UnblockUserAtInformationPublic
{
    public class UnblockUserAtInformationPublicCommandHandler : IRequestHandler<UnblockUserAtInformationPublicCommand>
    {
        private IMFDbContext _context;

        public UnblockUserAtInformationPublicCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UnblockUserAtInformationPublicCommand request, CancellationToken cancellationToken)
        {
            AppUser unblockingUser = await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken);
            if (unblockingUser == null)
                throw new EntityNotFoundException(nameof(AppUser), request.UserId);
            InformationPublic infoPublic = await _context.InformationPublics
                .Include(club => club.BlockedUsers)
                .FirstOrDefaultAsync(club => club.Id == request.PublicId, cancellationToken);
            if (infoPublic == null)
                throw new EntityNotFoundException(nameof(InformationPublic), request.PublicId);
            if (!infoPublic.BlockedUsers.Contains(unblockingUser))
                throw new DestructiveActionException("Пользователь не заблокирован.");
            if (infoPublic.OwnerId != request.IssuerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            infoPublic.BlockedUsers.Remove(unblockingUser);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
