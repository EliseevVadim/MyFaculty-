using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using MyFaculty.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.BanInformationPublic
{
    public class BanInformationPublicCommandHandler : IRequestHandler<BanInformationPublicCommand>
    {
        private IMFDbContext _context;

        public BanInformationPublicCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(BanInformationPublicCommand request, CancellationToken cancellationToken)
        {
            InformationPublic bannedPublic = await _context.InformationPublics.FindAsync(new object[] { request.BannedPublicId });
            if (bannedPublic == null)
                throw new EntityNotFoundException(nameof(InformationPublic), request.BannedPublicId);
            if (bannedPublic.IsBanned)
                throw new DestructiveActionException("Вы не можете заблокировать это сообщество, поскольку оно уже заблокировано.");
            bannedPublic.IsBanned = true;
            InformationPublicBanReport banReport = new InformationPublicBanReport()
            {
                PublicId = request.BannedPublicId,
                AdministratorId = request.AdministratorId,
                Reason = request.Reason,
                PerformedAction = BanAction.Banned,
                Created = DateTime.Now
            };
            await _context.InformationPublicsBansReports.AddAsync(banReport, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
