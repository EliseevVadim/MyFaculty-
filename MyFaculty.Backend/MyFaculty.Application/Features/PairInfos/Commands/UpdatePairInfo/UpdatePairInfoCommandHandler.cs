using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.PairInfos.Commands.UpdatePairInfo
{
    public class UpdatePairInfoCommandHandler : IRequestHandler<UpdatePairInfoCommand, PairInfoViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public UpdatePairInfoCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PairInfoViewModel> Handle(UpdatePairInfoCommand request, CancellationToken cancellationToken)
        {
            PairInfo pairInfo = await _context.PairInfos.FirstOrDefaultAsync(pairInfo => pairInfo.Id == request.Id, cancellationToken);
            if (pairInfo == null)
                throw new EntityNotFoundException(nameof(PairInfo), request.Id);
            pairInfo.PairNumber = request.PairNumber;
            pairInfo.StartTime = request.StartTime;
            pairInfo.EndTime = request.EndTime;
            pairInfo.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<PairInfoViewModel>(pairInfo);
        }
    }
}
