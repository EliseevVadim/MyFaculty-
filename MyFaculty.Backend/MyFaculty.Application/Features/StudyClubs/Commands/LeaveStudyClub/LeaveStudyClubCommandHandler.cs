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

namespace MyFaculty.Application.Features.StudyClubs.Commands.LeaveStudyClub
{
    public class LeaveStudyClubCommandHandler : IRequestHandler<LeaveStudyClubCommand>
    {
        private IMFDbContext _context;

        public LeaveStudyClubCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(LeaveStudyClubCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);
            if (user == null)
                throw new EntityNotFoundException(nameof(AppUser), request.UserId);
            StudyClub club = await _context.StudyClubs
                .Include(club => club.Members)       
                .Include(club => club.Moderators)
                .FirstOrDefaultAsync(club => club.Id == request.StudyClubId, cancellationToken);
            if (club == null)
                throw new EntityNotFoundException(nameof(StudyClub), request.StudyClubId);
            if (club.OwnerId == user.Id)
                throw new DestructiveActionException("Вы не можете покинуть сообщество, являясь его " +
                    "владельцем. Делегируйте эту роль кому-нибудь перед выходом из сообщества.");
            club.Members.Remove(user);
            club.Moderators.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
