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

namespace MyFaculty.Application.Features.InformationPublics.Commands.JoinInformationPublic
{
    public class JoinInformationPublicCommandHandler : IRequestHandler<JoinInformationPublicCommand>
    {
        private IMFDbContext _context;

        public JoinInformationPublicCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(JoinInformationPublicCommand request, CancellationToken cancellationToken)
        {
            InformationPublic informationPublic = await _context.InformationPublics
                .Include(infoPublic => infoPublic.BlockedUsers)
                .FirstOrDefaultAsync(infoPublic => infoPublic.Id == request.PublicId, cancellationToken);
            if (informationPublic == null || informationPublic.IsBanned)
                throw new EntityNotFoundException(nameof(InformationPublic), request.PublicId);
            if (informationPublic.BlockedUsers.Select(user => user.Id).Contains(request.UserId))
                throw new DestructiveActionException("Вы не можете вступить в это сообщество, так как были там заблокированы");
            string query = $"INSERT INTO usersatinformationpublics (MembersId, InformationPublicsId) VALUES ({request.UserId}, {request.PublicId})";
            await _context.ExecuteSqlRawAsync(query);
            return Unit.Value;
        }
    }
}
