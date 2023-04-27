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

namespace MyFaculty.Application.Features.InformationPublics.Commands.AddModeratorToInformationPublic
{
    public class AddModeratorToInformationPublicCommandHandler : IRequestHandler<AddModeratorToInformationPublicCommand, int>
    {
        private IMFDbContext _context;

        public AddModeratorToInformationPublicCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(AddModeratorToInformationPublicCommand request, CancellationToken cancellationToken)
        {
            AppUser newModerator = await _context.Users.FindAsync(new object[] { request.ModeratorId }, cancellationToken);
            if (newModerator == null)
                throw new EntityNotFoundException(nameof(AppUser), request.ModeratorId);
            InformationPublic infoPublic = await _context.InformationPublics
                .Include(infoPublic => infoPublic.Moderators)
                .Include(infoPublic => infoPublic.Members)
                .FirstOrDefaultAsync(club => club.Id == request.InformationPublicId, cancellationToken);
            if (infoPublic == null || infoPublic.IsBanned)
                throw new EntityNotFoundException(nameof(StudyClub), request.InformationPublicId);
            if (request.IssuerId != infoPublic.OwnerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            if (!infoPublic.Members.Contains(newModerator))
                throw new DestructiveActionException("Перед становлением модератором пользователь должен состоять в сообществе.");
            if (infoPublic.Moderators.Contains(newModerator))
                throw new DestructiveActionException("Ошибка. Пользователь уже является модератором.");
            infoPublic.Moderators.Add(newModerator);
            Notification notification = new Notification()
            {
                UserId = request.ModeratorId,
                TextContent = $"Вы были назаначены модератором в информационном сообществе \"{infoPublic.PublicName}\"",
                ReturnUrl = $"/public{request.InformationPublicId}",
                Created = DateTime.Now
            };
            await _context.Notifications.AddAsync(notification, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return request.ModeratorId;
        }
    }
}
