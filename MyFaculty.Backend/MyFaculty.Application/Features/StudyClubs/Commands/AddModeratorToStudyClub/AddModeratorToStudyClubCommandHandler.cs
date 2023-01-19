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

namespace MyFaculty.Application.Features.StudyClubs.Commands.AddModeratorToStudyClub
{
    public class AddModeratorToStudyClubCommandHandler : IRequestHandler<AddModeratorToStudyClubCommand>
    {
        private IMFDbContext _context;

        public AddModeratorToStudyClubCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddModeratorToStudyClubCommand request, CancellationToken cancellationToken)
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
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
