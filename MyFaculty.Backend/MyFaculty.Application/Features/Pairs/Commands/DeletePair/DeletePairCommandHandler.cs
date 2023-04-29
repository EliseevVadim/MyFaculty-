using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Pairs.Commands.DeletePair
{
    public class DeletePairCommandHandler : IRequestHandler<DeletePairCommand>
    {
        private readonly IMFDbContext _context;

        public DeletePairCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePairCommand request, CancellationToken cancellationToken)
        {
            Pair pair = await _context.Pairs.FindAsync(new object[] { request.Id }, cancellationToken);
            if (pair == null)
                throw new EntityNotFoundException(nameof(Pair), request.Id);
            _context.Pairs.Remove(pair);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
