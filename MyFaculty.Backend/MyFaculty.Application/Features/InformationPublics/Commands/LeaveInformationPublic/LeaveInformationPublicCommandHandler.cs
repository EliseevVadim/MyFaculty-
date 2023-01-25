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

namespace MyFaculty.Application.Features.InformationPublics.Commands.LeaveInformationPublic
{
    public class LeaveInformationPublicCommandHandler : IRequestHandler<LeaveInformationPublicCommand>
    {
        private IMFDbContext _context;

        public LeaveInformationPublicCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(LeaveInformationPublicCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _context.Users
                .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);
            if (user == null)
                throw new EntityNotFoundException(nameof(AppUser), request.UserId);
            InformationPublic infoPublic = await _context.InformationPublics
                .Include(infoPublic => infoPublic.Members)
                .FirstOrDefaultAsync(club => club.Id == request.PublicId, cancellationToken);
            if (infoPublic == null)
                throw new EntityNotFoundException(nameof(InformationPublic), request.PublicId);
            if (infoPublic.OwnerId == user.Id)
                throw new DestructiveActionException("Вы не можете покинуть сообщество, являясь его " +
                    "владельцем. Делегируйте эту роль кому-нибудь перед выходом из сообщества.");
            infoPublic.Members.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
