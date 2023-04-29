using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.ScienceRanks.Commands.DeleteScienceRank
{
    public class DeleteScienceRankCommandHandler : IRequestHandler<DeleteScienceRankCommand>
    {
        private readonly IMFDbContext _context;

        public DeleteScienceRankCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteScienceRankCommand request, CancellationToken cancellationToken)
        {
            ScienceRank rank = await _context.ScienceRanks.FindAsync(new object[] { request.Id }, cancellationToken);
            if (rank == null)
                throw new EntityNotFoundException(nameof(ScienceRank), request.Id);
            _context.ScienceRanks.Remove(rank);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
