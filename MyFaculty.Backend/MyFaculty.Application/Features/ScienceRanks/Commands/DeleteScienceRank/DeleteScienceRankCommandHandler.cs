using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.ScienceRanks.Commands.DeleteScienceRank
{
    public class DeleteScienceRankCommandHandler : IRequestHandler<DeleteScienceRankCommand>
    {
        private IMFDbContext _context;

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
