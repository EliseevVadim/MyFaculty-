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

namespace MyFaculty.Application.Features.InformationPublics.Commands.UnbanInformationPublic
{
    public class UnbanInformationPublicCommandHandler : IRequestHandler<UnbanInformationPublicCommand>
    {
        private IMFDbContext _context;

        public UnbanInformationPublicCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UnbanInformationPublicCommand request, CancellationToken cancellationToken)
        {
            InformationPublic unbannedPublic = await _context.InformationPublics.FindAsync(new object[] { request.UnbannedPublicId });
            if (unbannedPublic == null)
                throw new EntityNotFoundException(nameof(InformationPublic), request.UnbannedPublicId);
            if (!unbannedPublic.IsBanned)
                throw new DestructiveActionException("Вы не можете разблокировать это сообщество, поскольку оно не заблокировано.");
            unbannedPublic.IsBanned = false;
            InformationPublicBanReport unbanReport = new InformationPublicBanReport()
            {
                PublicId = request.UnbannedPublicId,
                AdministratorId = request.AdministratorId,
                Reason = request.Reason,
                PerformedAction = BanAction.Unbanned,
                Created = DateTime.Now
            };
            await _context.InformationPublicsBansReports.AddAsync(unbanReport, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
