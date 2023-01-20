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

namespace MyFaculty.Application.Features.StudyClubs.Commands.AddUsersToStudyClubByGroup
{
    public class AddUsersToStudyClubByGroupCommandHandler : IRequestHandler<AddUsersToStudyClubByGroupCommand>
    {
        private IMFDbContext _context;

        public AddUsersToStudyClubByGroupCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddUsersToStudyClubByGroupCommand request, CancellationToken cancellationToken)
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
            club.Members.AddRange(usersFromGroup);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
