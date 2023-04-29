using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Commands.AddModeratorToStudyClub
{
    public class AddModeratorToStudyClubCommandHandler : IRequestHandler<AddModeratorToStudyClubCommand, int>
    {
        private IMFDbContext _context;

        public AddModeratorToStudyClubCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(AddModeratorToStudyClubCommand request, CancellationToken cancellationToken)
        {
            AppUser newModerator = await _context.Users.FindAsync(new object[] { request.ModeratorId }, cancellationToken);
            if (newModerator == null)
                throw new EntityNotFoundException(nameof(AppUser), request.ModeratorId);
            StudyClub club = await _context.StudyClubs
                .Include(club => club.Moderators)
                .Include(club => club.Members)
                .FirstOrDefaultAsync(club => club.Id == request.StudyClubId, cancellationToken);
            if (club == null)
                throw new EntityNotFoundException(nameof(StudyClub), request.StudyClubId);
            if (request.IssuerId != club.OwnerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            if (!club.Members.Contains(newModerator))
                throw new DestructiveActionException("Перед становлением модератором пользователь должен состоять в сообществе.");
            if (club.Moderators.Contains(newModerator))
                throw new DestructiveActionException("Ошибка. Пользователь уже является модератором.");
            club.Moderators.Add(newModerator);
            Notification notification = new Notification()
            {
                UserId = request.ModeratorId,
                TextContent = $"Вы были назаначены модератором в сообществе курса \"{club.ClubName}\"",
                ReturnUrl = $"/clubs/{request.StudyClubId}",
                Created = DateTime.Now
            };
            await _context.Notifications.AddAsync(notification, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return request.ModeratorId;
        }
    }
}
