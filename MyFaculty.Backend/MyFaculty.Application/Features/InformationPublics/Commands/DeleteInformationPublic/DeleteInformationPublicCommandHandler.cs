using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.DeleteInformationPublic
{
    public class DeleteInformationPublicCommandHandler : IRequestHandler<DeleteInformationPublicCommand>
    {
        private readonly IMFDbContext _context;

        public DeleteInformationPublicCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteInformationPublicCommand request, CancellationToken cancellationToken)
        {
            InformationPublic infoPublic = await _context.InformationPublics.FindAsync(new object[] { request.Id }, cancellationToken);
            if (infoPublic == null)
                throw new EntityNotFoundException(nameof(InformationPublic), request.Id);
            AppUser owner = await _context.Users.FindAsync(new object[] { request.IssuerId }, cancellationToken);
            if (owner == null)
                throw new EntityNotFoundException(nameof(AppUser), request.IssuerId);
            if (infoPublic.OwnerId != request.IssuerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            _context.InformationPublics.Remove(infoPublic);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
