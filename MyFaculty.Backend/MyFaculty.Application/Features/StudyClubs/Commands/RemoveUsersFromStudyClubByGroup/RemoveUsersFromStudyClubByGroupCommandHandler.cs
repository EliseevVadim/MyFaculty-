using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Commands.RemoveUsersFromStudyClubByGroup
{
    public class RemoveUsersFromStudyClubByGroupCommandHandler : IRequestHandler<RemoveUsersFromStudyClubByGroupCommand>
    {
        private IMFDbContext _context;

        public RemoveUsersFromStudyClubByGroupCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveUsersFromStudyClubByGroupCommand request, CancellationToken cancellationToken)
        {
            StudyClub club = await _context.StudyClubs
                .Include(club => club.Members)
                .FirstOrDefaultAsync(club => club.Id == request.StudyClubId, cancellationToken);
            if (club == null)
                throw new EntityNotFoundException(nameof(StudyClub), request.StudyClubId);
            if (club.OwnerId != request.IssuerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            List<AppUser> usersFromGroup = await _context.Users
                .Where(user => user.GroupId == request.GroupId)
                .ToListAsync(cancellationToken);
            club.Members.RemoveAll(member => usersFromGroup.Contains(member));
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
