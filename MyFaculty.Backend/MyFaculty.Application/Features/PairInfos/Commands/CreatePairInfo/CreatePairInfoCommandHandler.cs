using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.PairInfos.Commands.CreatePairInfo
{
    public class CreatePairInfoCommandHandler : IRequestHandler<CreatePairInfoCommand, PairInfoViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public CreatePairInfoCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PairInfoViewModel> Handle(CreatePairInfoCommand request, CancellationToken cancellationToken)
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
            return _mapper.Map<PairInfoViewModel>(pairInfo);
        }
    }
}
