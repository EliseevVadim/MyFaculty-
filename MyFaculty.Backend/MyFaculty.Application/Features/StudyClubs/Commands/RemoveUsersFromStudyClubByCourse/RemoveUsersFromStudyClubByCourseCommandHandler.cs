using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Commands.RemoveUsersFromStudyClubByCourse
{
    public class RemoveUsersFromStudyClubByCourseCommandHandler : IRequestHandler<RemoveUsersFromStudyClubByCourseCommand>
    {
        private IMFDbContext _context;

        public RemoveUsersFromStudyClubByCourseCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveUsersFromStudyClubByCourseCommand request, CancellationToken cancellationToken)
        {
            StudyClub club = await _context.StudyClubs
                .Include(club => club.Members)
                .FirstOrDefaultAsync(club => club.Id == request.StudyClubId, cancellationToken);
            if (club == null)
                throw new EntityNotFoundException(nameof(StudyClub), request.StudyClubId);
            if (club.OwnerId != request.IssuerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            List<AppUser> usersFromCourse = await _context.Users
                .Where(user => user.CourseId == request.CourseId)
                .ToListAsync(cancellationToken);
            club.Members.RemoveAll(member => usersFromCourse.Contains(member));
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
