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

namespace MyFaculty.Application.Features.StudyClubs.Commands.DemoteStudyClubModerator
{
    public class DemoteStudyClubModeratorCommandHandler : IRequestHandler<DemoteStudyClubModeratorCommand>
    {
        private IMFDbContext _context;

        public DemoteStudyClubModeratorCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DemoteStudyClubModeratorCommand request, CancellationToken cancellationToken)
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
            if (!club.Moderators.Contains(demotingModerator))
                throw new DestructiveActionException("Пользователь не является модератором сообщества.");
            club.Moderators.Remove(demotingModerator);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
