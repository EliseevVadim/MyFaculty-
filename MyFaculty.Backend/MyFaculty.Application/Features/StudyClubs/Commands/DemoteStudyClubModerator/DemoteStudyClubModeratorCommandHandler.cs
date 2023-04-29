using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Commands.DemoteStudyClubModerator
{
    public class DemoteStudyClubModeratorCommandHandler : IRequestHandler<DemoteStudyClubModeratorCommand, int>
    {
        private readonly IMFDbContext _context;

        public DemoteStudyClubModeratorCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DemoteStudyClubModeratorCommand request, CancellationToken cancellationToken)
        {
            AppUser demotingModerator = await _context.Users.FindAsync(new object[] { request.ModeratorId }, cancellationToken);
            if (demotingModerator == null)
                throw new EntityNotFoundException(nameof(AppUser), request.ModeratorId);
            StudyClub club = await _context.StudyClubs
                .Include(club => club.Moderators)
                .FirstOrDefaultAsync(club => club.Id == request.StudyClubId, cancellationToken);
            if (club == null)
                throw new EntityNotFoundException(nameof(StudyClub), request.StudyClubId);
            if (request.IssuerId != club.OwnerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            if (request.IssuerId == request.ModeratorId)
                throw new DestructiveActionException("Вы не можете понизить себя, будучи владельцем сообщества.");
            if (!club.Moderators.Contains(demotingModerator))
                throw new DestructiveActionException("Пользователь не является модератором сообщества.");
            club.Moderators.Remove(demotingModerator);
            Notification notification = new Notification()
            {
                UserId = request.ModeratorId,
                TextContent = $"Вы были сняты с должности модератора в сообществе курса \"{club.ClubName}\"",
                ReturnUrl = $"/clubs/{request.StudyClubId}",
                Created = DateTime.Now
            };
            await _context.Notifications.AddAsync(notification, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return request.ModeratorId;
        }
    }
}
