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

namespace MyFaculty.Application.Features.InformationPublics.Commands.DemoteInformationPublicModerator
{
    public class DemoteInformationPublicModeratorCommandHandler : IRequestHandler<DemoteInformationPublicModeratorCommand, int>
    {
        private IMFDbContext _context;

        public DemoteInformationPublicModeratorCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DemoteInformationPublicModeratorCommand request, CancellationToken cancellationToken)
        {
            AppUser demotingModerator = await _context.Users.FindAsync(new object[] { request.ModeratorId }, cancellationToken);
            if (demotingModerator == null)
                throw new EntityNotFoundException(nameof(AppUser), request.ModeratorId);
            InformationPublic infoPublic = await _context.InformationPublics
                .Include(infoPublic => infoPublic.Moderators)
                .FirstOrDefaultAsync(club => club.Id == request.InformationPublicId, cancellationToken);
            if (infoPublic == null || infoPublic.IsBanned)
                throw new EntityNotFoundException(nameof(StudyClub), request.InformationPublicId);
            if (request.IssuerId != infoPublic.OwnerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            if (request.IssuerId == request.ModeratorId)
                throw new DestructiveActionException("Вы не можете понизить себя, будучи владельцем сообщества.");
            if (!infoPublic.Moderators.Contains(demotingModerator))
                throw new DestructiveActionException("Пользователь не является модератором сообщества.");
            infoPublic.Moderators.Remove(demotingModerator);
            Notification notification = new Notification()
            {
                UserId = request.ModeratorId,
                TextContent = $"Вы были сняты с должности модератора в информационном сообществе \"{infoPublic.PublicName}\"",
                ReturnUrl = $"/public{request.InformationPublicId}",
                Created = DateTime.Now
            };
            await _context.Notifications.AddAsync(notification, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return request.ModeratorId;
        }
    }
}
