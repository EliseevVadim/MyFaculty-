using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Pairs.Commands.CreatePair
{
    public class CreatePairCommandHandler : IRequestHandler<CreatePairCommand, Pair>
    {
        private IMFDbContext _context;

        public CreatePairCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Pair> Handle(CreatePairCommand request, CancellationToken cancellationToken)
        {
            Pair pair = new Pair()
            {
                PairName = request.PairName,
                PairInfoId = request.PairInfoId,
                DayOfWeekId = request.DayOfWeekId,
                DisciplineId = request.DisciplineId,
                TeacherId = request.TeacherId,
                AuditoriumId = request.AuditriumId,
                PairRepeatingId = request.PairRepeatingId,
                GroupId = request.GroupId,
                Created = DateTime.Now
            };
            await _context.Pairs.AddAsync(pair, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return pair;
        }
    }
}
