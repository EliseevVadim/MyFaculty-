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

namespace MyFaculty.Application.Features.PairInfos.Commands.UpdatePairInfo
{
    public class UpdatePairInfoCommandHandler : IRequestHandler<UpdatePairInfoCommand, PairInfo>
    {
        private IMFDbContext _context;

        public UpdatePairInfoCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<PairInfo> Handle(UpdatePairInfoCommand request, CancellationToken cancellationToken)
        {
            PairInfo pairInfo = await _context.PairInfos.FirstOrDefaultAsync(pairInfo => pairInfo.Id == request.Id, cancellationToken);
            if (pairInfo == null)
                throw new EntityNotFoundException(nameof(PairInfo), request.Id);
            pairInfo.PairNumber = request.PairNumber;
            pairInfo.StartTime = request.StartTime;
            pairInfo.EndTime = request.EndTime;
            pairInfo.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return pairInfo;
        }
    }
}
