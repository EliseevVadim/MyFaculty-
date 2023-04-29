using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Regions.Commands.DeleteRegion
{
    public class DeleteRegionCommandHandler : IRequestHandler<DeleteRegionCommand>
    {
        private readonly IMFDbContext _context;

        public DeleteRegionCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
        {
            Region region = await _context.Regions.FindAsync(new object[] { request.Id }, cancellationToken);
            if (region == null)
                throw new EntityNotFoundException(nameof(Region), request.Id);
            _context.Regions.Remove(region);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
