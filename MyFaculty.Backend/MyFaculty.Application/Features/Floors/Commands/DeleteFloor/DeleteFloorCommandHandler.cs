using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Floors.Commands.DeleteFloor
{
    public class DeleteFloorCommandHandler : IRequestHandler<DeleteFloorCommand>
    {
        private readonly IMFDbContext _context;

        public DeleteFloorCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteFloorCommand request, CancellationToken cancellationToken)
        {
            Floor floor = await _context.Floors.FindAsync(new object[] { request.Id }, cancellationToken);
            if (floor == null)
                throw new EntityNotFoundException(nameof(Floor), request.Id);
            _context.Floors.Remove(floor);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
