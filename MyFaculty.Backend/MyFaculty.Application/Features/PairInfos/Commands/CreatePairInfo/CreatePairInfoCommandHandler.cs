using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.PairInfos.Commands.CreatePairInfo
{
    public class CreatePairInfoCommandHandler : IRequestHandler<CreatePairInfoCommand, PairInfo>
    {
        private IMFDbContext _context;

        public CreatePairInfoCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<PairInfo> Handle(CreatePairInfoCommand request, CancellationToken cancellationToken)
        {
            PairInfo pairInfo = new PairInfo()
            {
                PairNumber = request.PairNumber,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Created = DateTime.Now
            };
            await _context.PairInfos.AddAsync(pairInfo, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return pairInfo;
        }
    }
}
