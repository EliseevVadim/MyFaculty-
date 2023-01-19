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

namespace MyFaculty.Application.Features.StudyClubs.Commands.RemoveUserFromStudyClub
{
    public class RemoveUserFromStudyClubCommandHandler : IRequestHandler<RemoveUserFromStudyClubCommand>
    {
        private IMFDbContext _context;

        public RemoveUserFromStudyClubCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveUserFromStudyClubCommand request, CancellationToken cancellationToken)
        {
            AppUser removingUser = await _context.Users.FindAsync(new object[] { request.RemovingUserId }, cancellationToken);
            if (removingUser == null)
                throw new EntityNotFoundException(nameof(AppUser), request.RemovingUserId);
            StudyClub club = await _context.StudyClubs
                .Include(club => club.Moderators)
                .Include(club => club.Members)
                .FirstOrDefaultAsync(club => club.Id == request.StudyClubId, cancellationToken);
            if (club == null)
                throw new EntityNotFoundException(nameof(StudyClub), request.StudyClubId);
            if (!club.Members.Contains(removingUser))
                throw new DestructiveActionException("Пользователь не является членом сообщества.");
            if (club.Moderators.Contains(removingUser))
                throw new DestructiveActionException("Вы не можете исключить модератора из сообщества. Для начала понизьте его до обычного пользователя.");
            if (!club.Moderators.Select(user => user.Id).Contains(request.IssuerId))
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            club.Members.Remove(removingUser);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
