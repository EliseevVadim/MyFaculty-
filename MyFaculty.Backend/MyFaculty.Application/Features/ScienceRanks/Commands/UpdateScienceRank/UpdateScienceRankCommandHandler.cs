using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using System.Threading.Tasks;
using MyFaculty.Domain.Entities;
using System.Threading;
using MyFaculty.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;

namespace MyFaculty.Application.Features.ScienceRanks.Commands.UpdateScienceRank
{
    public class UpdateScienceRankCommandHandler : IRequestHandler<UpdateScienceRankCommand, ScienceRank>
    {
        private IMFDbContext _context;

        public UpdateScienceRankCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<ScienceRank> Handle(UpdateScienceRankCommand request, CancellationToken cancellationToken)
        {
            ScienceRank rank = await _context.ScienceRanks
                .FirstOrDefaultAsync(rank => rank.Id == request.Id, cancellationToken);
            if (rank == null)
                throw new EntityNotFoundException(nameof(ScienceRank), request.Id);
            rank.RankName = request.RankName;
            rank.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return rank;
        }
    }
}
