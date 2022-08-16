using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Pairs.Commands.UpdatePair
{
    public class UpdatePairCommandHandler : IRequestHandler<UpdatePairCommand, Pair>
    {
        private IMFDbContext _context;

        public UpdatePairCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Pair> Handle(UpdatePairCommand request, CancellationToken cancellationToken)
        {
            Pair pair = await _context.Pairs.FirstOrDefaultAsync(pair => pair.Id == request.Id, cancellationToken);
            if (pair == null)
                throw new EntityNotFoundException(nameof(Pair), request.Id);
            pair.PairName = request.PairName;
            pair.PairInfoId = request.PairInfoId;
            pair.DayOfWeekId = request.DayOfWeekId;
            pair.DisciplineId = request.DisciplineId;
            pair.TeacherId = request.TeacherId;
            pair.AuditoriumId = request.AuditriumId;
            pair.PairRepeatingId = request.PairRepeatingId;
            pair.GroupId = request.GroupId;
            pair.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return pair;
        }
    }
}
