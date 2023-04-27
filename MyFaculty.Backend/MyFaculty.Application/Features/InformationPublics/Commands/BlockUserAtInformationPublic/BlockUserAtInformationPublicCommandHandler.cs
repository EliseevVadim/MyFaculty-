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

namespace MyFaculty.Application.Features.InformationPublics.Commands.BlockUserAtInformationPublic
{
    public class BlockUserAtInformationPublicCommandHandler : IRequestHandler<BlockUserAtInformationPublicCommand>
    {
        private IMFDbContext _context;

        public BlockUserAtInformationPublicCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(BlockUserAtInformationPublicCommand request, CancellationToken cancellationToken)
        {
            AppUser blockingUser = await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken);
            if (blockingUser == null)
                throw new EntityNotFoundException(nameof(AppUser), request.UserId);
            InformationPublic infoPublic = await _context.InformationPublics
                .Include(infoPublic => infoPublic.Moderators)
                .Include(infoPublic => infoPublic.Members)
                .Include(infoPublic => infoPublic.BlockedUsers)
                .FirstOrDefaultAsync(club => club.Id == request.PublicId, cancellationToken);
            if (infoPublic == null || infoPublic.IsBanned)
                throw new EntityNotFoundException(nameof(InformationPublic), request.PublicId);
            if (!infoPublic.Members.Contains(blockingUser))
                throw new DestructiveActionException("Пользователь не является членом сообщества.");
            if (!infoPublic.Moderators.Any(user => user.Id == request.IssuerId))
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            if (infoPublic.OwnerId == request.UserId)
                throw new UnauthorizedActionException("Вы не можете заблокировать владельца сообщества.");
            if (infoPublic.Moderators.Any(user => user.Id == request.UserId))
                throw new DestructiveActionException("Вы не можете заблокировать и исключить модератора. Сначала понизьте его до обычного пользователя.");
            infoPublic.Members.Remove(blockingUser);
            infoPublic.BlockedUsers.Add(blockingUser);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
