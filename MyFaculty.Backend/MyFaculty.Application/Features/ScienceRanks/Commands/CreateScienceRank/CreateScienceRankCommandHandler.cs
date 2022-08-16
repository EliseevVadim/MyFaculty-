using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.ScienceRanks.Commands.CreateScienceRank
{
    public class CreateScienceRankCommandHandler : IRequestHandler<CreateScienceRankCommand, ScienceRank>
    {
        private IMFDbContext _context;

        public CreateScienceRankCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<ScienceRank> Handle(CreateScienceRankCommand request, CancellationToken cancellationToken)
        {
            ScienceRank rank = new ScienceRank
            {
                RankName = request.RankName,
                Created = DateTime.Now
            };
            await _context.ScienceRanks.AddAsync(rank);
            await _context.SaveChangesAsync(cancellationToken);
            return rank;
        }
    }
}
